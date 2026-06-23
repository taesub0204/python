using System;

namespace ConsoleApp06_26
{
    internal class Box
    {
        private int width;   // 가로
        private int height;  // 세로

        public Box(int width, int height)
        {
            // new Box(10,10)
            // 매개변수 width=10, height=10

            if (width > 0 && height > 0)
            {
                this.width = width;     // 객체 width = 10
                this.height = height;   // 객체 height = 10
            }
        }

        public int GetArea()
        {
            // 현재 width=10, height=10
            return this.width * this.height; // 10*10 → 100 반환
        }

        public int GetWidth()
        {
            return this.width; // width 값 반환
        }

        public int GetHeight()
        {
            return this.height; // height 값 반환
        }

        public void SetWidth(int width)
        {
            // 예: SetWidth(20)

            if (width > 0)
            {
                this.width = width; // 객체 width를 20으로 변경
            }
        }

        public void SetHeight(int height)
        {
            // 예: SetHeight(30)

            if (height > 0)
            {
                this.height = height; // 객체 height를 30으로 변경
            }
        }
    }
}