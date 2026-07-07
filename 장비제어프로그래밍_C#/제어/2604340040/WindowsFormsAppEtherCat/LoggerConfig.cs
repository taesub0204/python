using System;
using System.IO;
using System.Windows.Forms;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace WindowsFormsAppEtherCat
{
    // log4net 로그를 화면의 TextBox에 실시간으로 뿌려주는 커스텀 어펜더.
    // [Z축추적]/[센서추적] 같은 DEBUG 도배는 안 보이게 Threshold를 INFO로 두고 씁니다.
    public class TextBoxAppender : AppenderSkeleton
    {
        private TextBox _target;
        private const int MaxChars = 20000; // 너무 길어지면 앞부분을 잘라 메모리/렌더 부담 방지

        public void SetTarget(TextBox target) { _target = target; }

        protected override void Append(LoggingEvent loggingEvent)
        {
            TextBox tb = _target;
            if (tb == null || tb.IsDisposed || !tb.IsHandleCreated) return;

            string line = RenderLoggingEvent(loggingEvent);
            try
            {
                tb.BeginInvoke(new Action(() =>
                {
                    if (tb.IsDisposed) return;
                    if (tb.TextLength > MaxChars)
                        tb.Text = tb.Text.Substring(tb.TextLength - MaxChars / 2);
                    tb.AppendText(line);
                    tb.SelectionStart = tb.TextLength;
                    tb.ScrollToCaret();
                }));
            }
            catch { /* 창이 닫히는 중이면 무시 */ }
        }
    }

    // log4net을 코드로 직접 설정합니다 (App.config에 <log4net> 섹션을 따로 등록할 필요 없음).
    public static class LoggerConfig
    {
        public static readonly ILog Log = LogManager.GetLogger("SemiE95");

        // 엑셀로 내보낼 때 이 파일을 읽습니다.
        public static string LogFilePath { get; private set; }

        // 화면 TextBox 출력용 어펜더(초기화 시 생성, 뷰가 준비되면 SetTarget으로 TextBox를 연결)
        private static TextBoxAppender _textBoxAppender;

        // SemiE95View 생성자에서 호출 — 로그를 이 TextBox에 실시간 출력합니다.
        public static void AttachTextBox(TextBox target)
        {
            if (_textBoxAppender != null) _textBoxAppender.SetTarget(target);
        }

        public static void Initialize()
        {
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDir);
            LogFilePath = Path.Combine(logDir, "semi_e95.log");

            var hierarchy = (Hierarchy)LogManager.GetRepository();

            // 시간|레벨|메시지 형식 - 나중에 엑셀로 내보낼 때 "|" 기준으로 열을 나눕니다.
            var layout = new PatternLayout("%date{yyyy-MM-dd HH:mm:ss}|%level|%message%newline");
            layout.ActivateOptions();

            var appender = new RollingFileAppender
            {
                AppendToFile = true,
                File = LogFilePath,
                Layout = layout,
                // 엑셀 내보내기(StreamReader, 기본 UTF-8)와 인코딩을 맞추기 위해 UTF-8(BOM)로 기록.
                // 기본값(시스템 ANSI/CP949)으로 쓰면 내보낸 엑셀에서 한글이 깨집니다.
                Encoding = new System.Text.UTF8Encoding(true),
                MaxSizeRollBackups = 10,
                MaximumFileSize = "5MB",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                StaticLogFileName = true,
                // 파일을 쓰는 순간에만 잠그기 때문에, 앱이 로그를 쓰는 도중에도 엑셀 내보내기(읽기)가 가능합니다.
                LockingModel = new FileAppender.MinimalLock()
            };
            appender.ActivateOptions();

            hierarchy.Root.AddAppender(appender);

            // 화면 TextBox 출력용 어펜더 (INFO 이상만 — DEBUG 추적 로그 도배 방지)
            _textBoxAppender = new TextBoxAppender
            {
                Layout = layout,
                Threshold = Level.Info
            };
            _textBoxAppender.ActivateOptions();
            hierarchy.Root.AddAppender(_textBoxAppender);

            hierarchy.Root.Level = Level.All;
            hierarchy.Configured = true;
        }
    }
}
