// PECVD Semiconductor Deposition Equipment Simulator
// Designed for Semiconductor SW Training

// DOM Elements
const inputPressure = document.getElementById('inputPressure');
const inputTemp = document.getElementById('inputTemp');
const inputRF = document.getElementById('inputRF');
const inputGasFlow = document.getElementById('inputGasFlow');
const inputNH3Flow = document.getElementById('inputNH3Flow');
const inputN2Flow = document.getElementById('inputN2Flow');
const inputProcessTime = document.getElementById('inputProcessTime');

const valPressure = document.getElementById('valPressure');
const valTemp = document.getElementById('valTemp');
const valRF = document.getElementById('valRF');
const valGasFlow = document.getElementById('valGasFlow');
const valNH3Flow = document.getElementById('valNH3Flow');
const valN2Flow = document.getElementById('valN2Flow');
const valProcessTime = document.getElementById('valProcessTime');

const boxPressure = document.getElementById('boxPressure');
const boxTemp = document.getElementById('boxTemp');
const boxRF = document.getElementById('boxRF');
const boxGasFlow = document.getElementById('boxGasFlow');
const boxNH3Flow = document.getElementById('boxNH3Flow');
const boxN2Flow = document.getElementById('boxN2Flow');
const boxProcessTime = document.getElementById('boxProcessTime');

const btnStart = document.getElementById('btnStart');
const btnStop = document.getElementById('btnStop');
const btnFault = document.getElementById('btnFault');
const btnSave = document.getElementById('btnSave');
const btnReset = document.getElementById('btnReset');

const metricThickness = document.getElementById('metricThickness');
const metricUniformity = document.getElementById('metricUniformity');
const metricDepRate = document.getElementById('metricDepRate');
const metricStatus = document.getElementById('metricStatus');
const metricResult = document.getElementById('metricResult');

const cardStatus = document.getElementById('cardStatus');
const cardResult = document.getElementById('cardResult');

const logTerminal = document.getElementById('logTerminal');
const chamberCanvas = document.getElementById('chamberCanvas');
const thicknessChart = document.getElementById('thicknessChart');
const uniformityChart = document.getElementById('uniformityChart');

// Overlay Elements
const overlaySiH4 = document.getElementById('overlaySiH4');
const overlayNH3 = document.getElementById('overlayNH3');
const overlayPlasma = document.getElementById('overlayPlasma');

// Recipe Buttons
const recipeSiN = document.getElementById('recipeSiN');
const recipeLowStressSiN = document.getElementById('recipeLowStressSiN');
const recipeSiO2 = document.getElementById('recipeSiO2');
const recipeaSi = document.getElementById('recipeaSi');
const chkAutoRun = document.getElementById('chkAutoRun');
const displayWaferId = document.getElementById('displayWaferId');
const selectSpeed = document.getElementById('selectSpeed');
const chkForceDefect = document.getElementById('chkForceDefect');
const inputDefectRate = document.getElementById('inputDefectRate');
const btnApplyDefectRate = document.getElementById('btnApplyDefectRate');
const statusDefectRate = document.getElementById('statusDefectRate');

// 2D Telemetry UI Elements
const telemetryOverlay2d = document.getElementById('telemetryOverlay2d');
const chamberOverlayHtml = document.getElementById('chamberOverlayHtml');
const telWafersPerSec = document.getElementById('telWafersPerSec');
const telTotalOk = document.getElementById('telTotalOk');
const telTotalNg = document.getElementById('telTotalNg');

// Simulation State Variables
let state = {
    pressure: 1.0,
    temp: 400,
    rfPower: 300,
    gasFlow: 100,
    nh3Flow: 150,
    n2Flow: 500,
    processTime: 120,
    
    elapsedTime: 0,
    thickness: 0.0,
    uniformity: 0.0,
    depositionRate: 0.0,
    status: 'IDLE', // IDLE, RUNNING, WARNING, FAULT, COMPLETE, UNLOADING, LOADING
    result: '-',    // -, OK, NG
    
    recipe: 'SiN',
    logHistory: [],     // All ticks for the CSV file
    chartData: [],      // History of thickness/uniformity for plotting
    simulationInterval: null,
    animationFrameId: null,
    
    // Fault Scenario Control
    faultActive: false,
    faultTimer: 0,
    faultType: null, // 'gas_drop', 'pressure_spike', or 'scenario'

    // Wafer Serial Cycle Control
    waferId: 1,
    waferOffsetX: 0, // Visual displacement of wafer in pixels
    waferCycleState: 'READY', // 'READY', 'RUNNING', 'UNLOADING', 'LOADING'
    defectRate: 10,
    stopReserved: false,
    
    // Session Yield Tracking (세션 수율 추적)
    sessionOk: 0,
    sessionNg: 0
};

// Canvas Particle and Animation Config
let plasmaOpacity = 0;
let heaterPulse = 0;
let particleEmissionRate = 0;

// Setup Canvas context (WebGL for chamber, 2D for charts)
let scene, camera, renderer, controls;
let chamberHousing, showerhead, susceptor, heaterCoils, wafer, film, plasmaCloud;
let plasmaLight, heaterLight, ambientLight, dirLight;
let particles3D = [];
const maxParticles = 80;

const thCtx = thicknessChart.getContext('2d');
const uniCtx = uniformityChart.getContext('2d');

// Initialize dimensions
function resizeCanvases() {
    const rect = chamberCanvas.getBoundingClientRect();
    
    // Resize WebGL camera & viewport
    if (camera && renderer) {
        camera.aspect = rect.width / rect.height;
        camera.updateProjectionMatrix();
        renderer.setSize(rect.width, rect.height, false);
    }
    
    // Resize 2D charts
    const resize2D = (canvas) => {
        const r = canvas.getBoundingClientRect();
        if (r.width === 0 || r.height === 0) return; // Prevent sizing to 0
        canvas.width = r.width * window.devicePixelRatio;
        canvas.height = r.height * window.devicePixelRatio;
    };
    resize2D(thicknessChart);
    resize2D(uniformityChart);
    drawCharts();
}

// Use ResizeObserver for more reliable resizing (e.g. device folding/unfolding)
const resizeObserver = new ResizeObserver(() => {
    // Debounce or just call directly, requestAnimationFrame prevents rapid firing issues
    requestAnimationFrame(resizeCanvases);
});

// Observe parent containers
const chamberViewport = document.querySelector('.chamber-viewport');
const lowerMainRow = document.querySelector('.lower-main-row');
if (chamberViewport) resizeObserver.observe(chamberViewport);
if (lowerMainRow) resizeObserver.observe(lowerMainRow);
// Also observe window just in case
window.addEventListener('resize', () => requestAnimationFrame(resizeCanvases));
setTimeout(resizeCanvases, 100);

// Preset Recipe Configurations
const recipes = {
    SiN: { pressure: 1.0, temp: 400, rfPower: 300, gasFlow: 100, nh3Flow: 150, n2Flow: 500, processTime: 120 },
    LowStressSiN: { pressure: 1.2, temp: 400, rfPower: 250, gasFlow: 85, nh3Flow: 240, n2Flow: 800, processTime: 180 },
    SiO2: { pressure: 1.1, temp: 380, rfPower: 280, gasFlow: 90, nh3Flow: 0, n2Flow: 600, processTime: 150 },
    aSi: { pressure: 0.9, temp: 300, rfPower: 150, gasFlow: 120, nh3Flow: 0, n2Flow: 400, processTime: 200 }
};

// Apply Recipe Values to UI Sliders
function applyRecipe(recipeName) {
    if (state.status === 'RUNNING' || state.status === 'WARNING') return;
    
    // If system is stuck in FAULT or COMPLETE, selecting a new recipe should prime it back to IDLE
    if (state.status === 'FAULT' || state.status === 'COMPLETE') {
        updateStatus('IDLE');
        btnStart.disabled = false;
        btnStop.disabled = true;
        btnFault.disabled = true;
        state.faultActive = false;
        cardResult.setAttribute('data-result', '-');
        metricResult.textContent = '-';
        addLog('sys', "System reset initiated by recipe selection (레시피 선택으로 시스템이 자동 초기화되었습니다).");
    }

    if (recipeName === 'custom') {
        state.recipe = 'custom';
        readInputs();
        addLog('sys', `System reset with custom parameters (커스텀 설정 유지 후 준비 완료).`);
        return;
    }
    
    state.recipe = recipeName;
    const config = recipes[recipeName];
    
    // Set Slider values
    inputPressure.value = config.pressure;
    inputTemp.value = config.temp;
    inputRF.value = config.rfPower;
    inputGasFlow.value = config.gasFlow;
    inputNH3Flow.value = config.nh3Flow;
    inputN2Flow.value = config.n2Flow;
    inputProcessTime.value = config.processTime;
    
    // Set Recipe Button Active State
    [recipeSiN, recipeLowStressSiN, recipeSiO2, recipeaSi].forEach(btn => btn.classList.remove('active'));
    if (recipeName === 'SiN') recipeSiN.classList.add('active');
    if (recipeName === 'LowStressSiN') recipeLowStressSiN.classList.add('active');
    if (recipeName === 'SiO2') recipeSiO2.classList.add('active');
    if (recipeName === 'aSi') recipeaSi.classList.add('active');
    
    // Read input values and validate ranges
    readInputs();
    addLog('sys', `Recipe loaded (레시피 로드됨): ${recipeName.toUpperCase()}. System primed (시스템 준비 완료).`);
}

recipeSiN.addEventListener('click', () => applyRecipe('SiN'));
recipeLowStressSiN.addEventListener('click', () => applyRecipe('LowStressSiN'));
recipeSiO2.addEventListener('click', () => applyRecipe('SiO2'));
recipeaSi.addEventListener('click', () => applyRecipe('aSi'));

// Input parameter limits configuration
const limits = {
    pressure: { minNormal: 0.8, maxNormal: 1.2, minLimit: 0.7, maxLimit: 1.3 },
    temp: { minNormal: 380, maxNormal: 420, minLimit: 350, maxLimit: 450 },
    rfPower: { minNormal: 250, maxNormal: 350, minLimit: 200, maxLimit: 400 },
    gasFlow: { minNormal: 80, maxNormal: 120, minLimit: 70, maxLimit: 140 }
};

// Check Parameter Status and Apply Colors
function checkParameterLimits(val, config, boxEl) {
    boxEl.classList.remove('warning', 'danger');
    if (val < config.minLimit || val > config.maxLimit) {
        boxEl.classList.add('danger');
        return 'danger';
    } else if (val < config.minNormal || val > config.maxNormal) {
        boxEl.classList.add('warning');
        return 'warning';
    }
    return 'normal';
}

// Read Current Values from Inputs
function readInputs() {
    state.pressure = parseFloat(inputPressure.value);
    state.temp = parseFloat(inputTemp.value);
    state.rfPower = parseFloat(inputRF.value);
    state.gasFlow = parseFloat(inputGasFlow.value);
    state.nh3Flow = parseFloat(inputNH3Flow.value);
    state.n2Flow = parseFloat(inputN2Flow.value);
    state.processTime = parseFloat(inputProcessTime.value);
    
    // Update Text Displays
    valPressure.textContent = state.pressure.toFixed(2);
    valTemp.textContent = state.temp;
    valRF.textContent = state.rfPower;
    valGasFlow.textContent = state.gasFlow;
    valNH3Flow.textContent = state.nh3Flow;
    valN2Flow.textContent = state.n2Flow;
    valProcessTime.textContent = state.processTime;
    
    // Validate Ranges & Set Warning UI Styles
    const pStatus = checkParameterLimits(state.pressure, limits.pressure, boxPressure);
    const tStatus = checkParameterLimits(state.temp, limits.temp, boxTemp);
    const rfStatus = checkParameterLimits(state.rfPower, limits.rfPower, boxRF);
    const gStatus = checkParameterLimits(state.gasFlow, limits.gasFlow, boxGasFlow);
    
    // Update Canvas Overlay numbers
    overlaySiH4.textContent = state.gasFlow;
    overlayNH3.textContent = state.nh3Flow;
    
    // Realtime calculations if idle
    if (state.status === 'IDLE') {
        const estThickness = calculateTotalThickness();
        const estUniformity = calculateUniformity();
        metricThickness.textContent = `~${estThickness.toFixed(1)}`;
        metricUniformity.textContent = `${estUniformity.toFixed(1)}`;
        metricDepRate.textContent = `${(estThickness / (state.processTime / 60)).toFixed(1)}`;
    }
    
    // Return overall health based on parameter ranges
    if (pStatus === 'danger' || tStatus === 'danger' || rfStatus === 'danger' || gStatus === 'danger') {
        return 'danger';
    } else if (pStatus === 'warning' || tStatus === 'warning' || rfStatus === 'warning' || gStatus === 'warning') {
        return 'warning';
    }
    return 'normal';
}

// Add event listeners to all sliders
[inputPressure, inputTemp, inputRF, inputGasFlow, inputNH3Flow, inputN2Flow, inputProcessTime].forEach(slider => {
    slider.addEventListener('input', () => {
        // Deselect active recipe since parameters were customized
        [recipeSiN, recipeLowStressSiN, recipeSiO2, recipeaSi].forEach(btn => btn.classList.remove('active'));
        state.recipe = 'custom';
        const health = readInputs();
        
        // If system is running and sliders are shifted, handle live parameter changes
        if (state.status === 'RUNNING' || state.status === 'WARNING') {
            if (health === 'danger') {
                triggerFault('Parameter limit exceeded during deposition.');
            } else if (health === 'warning') {
                updateStatus('WARNING');
                addLog('warn', `Warning: Parameter range deviation detected.`);
            } else {
                updateStatus('RUNNING');
            }
        }
    });
});

// Deposition Physics Formula Calculations
function calculateTotalThickness() {
    // 1. Calculate Base Deposition Rate (nm/min) using impact weights
    let rate = 250.0;
    
    // Temperature: 4 stars
    rate += (state.temp - 400) * 0.6;
    // RF Power: 5 stars
    rate += (state.rfPower - 300) * 0.4;
    // Pressure: 2 stars
    rate += (state.pressure - 1.0) * 20.0;
    // SiH4 Flow: 4 stars
    rate += (state.gasFlow - 100) * 0.6;
    // NH3 Flow: 1 star
    rate += (state.nh3Flow - 150) * 0.05;
    // N2 Flow: 1 star
    rate += (state.n2Flow - 500) * 0.01;
    // Process Time: 5 stars (time depletion effect on rate)
    rate += (state.processTime - 120) * -0.1;

    // Constrain rate to physical limits
    rate = Math.max(10, rate);

    // 2. Thickness (nm) = Rate * Time (Process Time: 5 stars impact on thickness)
    const thickness = rate * (state.processTime / 60.0);
    return Math.max(0, thickness);
}

function calculateUniformity() {
    // Uniformity (%) starts at 98.0 and degrades based on absolute deviations
    let uniformity = 98.0;
    
    // Pressure: 5 stars (huge impact if off by 0.2)
    uniformity -= Math.abs(state.pressure - 1.0) * 15.0;
    // N2 Flow: 4 stars
    uniformity -= Math.abs(state.n2Flow - 500) * 0.005;
    // Temperature: 3 stars
    uniformity -= Math.abs(state.temp - 400) * 0.03;
    // NH3 Flow: 3 stars
    uniformity -= Math.abs(state.nh3Flow - 150) * 0.01;
    // RF Power: 2 stars
    uniformity -= Math.abs(state.rfPower - 300) * 0.01;
    // SiH4 Flow: 2 stars
    uniformity -= Math.abs(state.gasFlow - 100) * 0.02;
    // Process Time: 1 star
    uniformity -= Math.abs(state.processTime - 120) * 0.005;
    
    return Math.max(0, Math.min(100, uniformity));
}

// Update UI Badge Status
function updateStatus(newStatus) {
    state.status = newStatus;
    
    // Map status code to bilingual label
    let label = newStatus;
    if (newStatus === 'IDLE') label = "IDLE (대기)";
    else if (newStatus === 'RUNNING') label = "RUNNING (진행)";
    else if (newStatus === 'WARNING') label = "WARNING (주의)";
    else if (newStatus === 'FAULT') label = "FAULT (설비 알람)";
    else if (newStatus === 'COMPLETE') label = "COMPLETE (공정 완료)";
    else if (newStatus === 'UNLOADING') label = "UNLOADING (웨이퍼 반출)";
    else if (newStatus === 'LOADING') label = "LOADING (웨이퍼 반입)";
    
    metricStatus.textContent = label;
    cardStatus.setAttribute('data-status', newStatus);
    
    if (newStatus === 'COMPLETE') {
        metricResult.textContent = state.result;
        cardResult.setAttribute('data-result', state.result);
    } else if (newStatus === 'FAULT') {
        metricResult.textContent = 'NG';
        cardResult.setAttribute('data-result', 'NG');
    } else {
        metricResult.textContent = '-';
        cardResult.setAttribute('data-result', '-');
    }
}

// Add system logs
function addLog(tag, message) {
    const now = new Date();
    const timeStr = `${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}:${String(now.getSeconds()).padStart(2, '0')}`;
    
    let tagClass = 'log-tag-sys';
    let tagText = '[SYS]';
    
    if (tag === 'dep') { tagClass = 'log-tag-dep'; tagText = '[DEP]'; }
    if (tag === 'warn') { tagClass = 'log-tag-warn'; tagText = '[WARN]'; }
    if (tag === 'err') { tagClass = 'log-tag-err'; tagText = '[ERR]'; }
    
    const entry = document.createElement('div');
    entry.className = 'log-entry';
    entry.innerHTML = `<span class="log-time">[${timeStr}]</span><span class="${tagClass}">${tagText}</span> ${message}`;
    
    logTerminal.appendChild(entry);
    logTerminal.scrollTop = logTerminal.scrollHeight;
}

// Simulation Loop Sub-stepping Engine for ultra high speedups (100x, 1000x, 10000x)
function startSimulationLoop() {
    if (state.simulationInterval) clearInterval(state.simulationInterval);
    
    const speed = parseFloat(selectSpeed.value);
    
    if (speed === 10000) {
        // ULTRA-HIGH SPEED BATCH MODE (초고속 배치 모드)
        // 1,000 Wafers per second = 20 wafers per 20ms callback (50Hz loop)
        let totalProcessedThisBatch = 0;
        let batchStartWaferId = state.waferId;
        
        // Hide 3D view, show 2D telemetry dashboard
        chamberCanvas.style.display = 'none';
        if (chamberOverlayHtml) chamberOverlayHtml.style.display = 'none';
        if (telemetryOverlay2d) telemetryOverlay2d.style.display = 'flex';
        
        // Track stats for the current running batch
        let totalOkAcc = 0;
        let totalNgAcc = 0;
        
        let batchOk = 0;
        let batchNg = 0;
        
        // Reset 2D stats display
        telTotalOk.textContent = "0";
        telTotalNg.textContent = "0";
        telWafersPerSec.textContent = "1,000";
        
        state.simulationInterval = setInterval(() => {
            if (state.status !== 'RUNNING' && state.status !== 'WARNING') {
                clearInterval(state.simulationInterval);
                return;
            }
            
            // Process 20 wafers in this 20ms frame
            for (let w = 0; w < 20; w++) {
                if (state.status !== 'RUNNING' && state.status !== 'WARNING') {
                    break;
                }
                const res = processOneWaferInstant();
                if (res === 'OK') {
                    batchOk++;
                    totalOkAcc++;
                } else {
                    batchNg++;
                    totalNgAcc++;
                }
                totalProcessedThisBatch++;
            }
            
            // Update UI with the last wafer's stats
            displayWaferId.textContent = 'W-' + String(state.waferId).padStart(2, '0');
            metricThickness.textContent = state.thickness.toFixed(1);
            metricUniformity.textContent = state.uniformity.toFixed(1);
            metricDepRate.textContent = state.depositionRate.toFixed(1);
            cardResult.setAttribute('data-result', state.result);
            metricResult.textContent = state.result;
            
            // Update 2D telemetry dashboard labels
            telTotalOk.textContent = totalOkAcc.toLocaleString();
            telTotalNg.textContent = totalNgAcc.toLocaleString();
            
            // Render the end state on the chart (just a flat line at the target thickness for speed)
            state.chartData = [
                { time: 0, thickness: 0, uniformity: state.uniformity },
                { time: state.processTime, thickness: state.thickness, uniformity: state.uniformity }
            ];
            drawCharts();
            
            // Every 50 ticks (1 second), log the batch summary to the terminal
            if (totalProcessedThisBatch % 1000 === 0) {
                addLog('sys', `[Batch Process] Processed 1,000 wafers (W-${batchStartWaferId} to W-${state.waferId - 1}). OK: ${batchOk} | NG: ${batchNg} (Yield Rate: ${((batchOk / 1000) * 100).toFixed(1)}%)`);
                batchStartWaferId = state.waferId;
                batchOk = 0;
                batchNg = 0;
            }
        }, 20);
    } else {
        // Make sure 3D view is restored
        chamberCanvas.style.display = 'block';
        if (chamberOverlayHtml) chamberOverlayHtml.style.display = 'block';
        if (telemetryOverlay2d) telemetryOverlay2d.style.display = 'none';
        
        if (speed <= 50) {
            const intervalMs = 1000 / speed;
            state.simulationInterval = setInterval(() => {
                simulationTick(true);
            }, intervalMs);
        } else {
            // Run physics sub-stepping (50Hz loop)
            const ticksPerCallback = Math.round(speed / 50);
            state.simulationInterval = setInterval(() => {
                for (let i = 0; i < ticksPerCallback; i++) {
                    if (state.status !== 'RUNNING' && state.status !== 'WARNING') {
                        break;
                    }
                    simulationTick(false);
                }
                drawCharts();
            }, 20);
        }
    }
}

// Simulation Control Actions
function startSimulation() {
    if (state.status === 'RUNNING' || state.status === 'WARNING') return;
    
    // Check initial parameters health
    const health = readInputs();
    if (health === 'danger') {
        addLog('err', "Abnormal launch parameters! Chamber check failed. Launch aborted.");
        alert("위험 범위의 파라미터가 있습니다. 입력값을 정상 범위로 조정해 주세요.");
        return;
    }
    
    // Reset simulation run variables
    state.elapsedTime = 0;
    state.thickness = 0.0;
    state.uniformity = calculateUniformity();
    state.chartData = [];
    state.faultActive = false;
    
    // Clear log history and session stats on first wafer OR when manually restarting
    // (Auto-run continues accumulating; manual Start after Stop begins a fresh session)
    if (state.waferId === 1 || !state._isAutoRun) {
        state.logHistory = [];
        state.sessionOk = 0;
        state.sessionNg = 0;
    }
    state._isAutoRun = false; // reset flag; auto-run will set it before calling startSimulation
    
    // Sync defect rate from UI input at simulation start (prevent stale values)
    const parsedRate = parseFloat(inputDefectRate.value);
    if (!isNaN(parsedRate) && parsedRate >= 0 && parsedRate <= 100) {
        state.defectRate = parsedRate;
        statusDefectRate.textContent = `Applied Defect Rate (적용 불량률): ${parsedRate}%`;
    }
    
    // Enable/Disable buttons
    btnStart.disabled = true;
    btnStop.disabled = false;
    btnFault.disabled = false;
    btnSave.disabled = true;
    btnReset.disabled = true;
    
    resetStopButton(); // Reset styling if it was previously reserved
    
    // Lock only process time during simulation, keep other parameters adjustable
    inputProcessTime.disabled = true;
    
    updateStatus('RUNNING');
    addLog('sys', "Deposition Sequence initiated (증착 공정 시퀀스가 시작되었습니다).");
    addLog('sys', "Stabilizing gas flow (가스 유량 안정화 중)...");
    
    // Warn if Force Defect override is active (user may have forgotten to uncheck)
    if (chkForceDefect.checked) {
        addLog('warn', `⚠️ [Force Defect ACTIVE] All wafers will be forced FAIL! Uncheck 'Force Defect' to use yield rate. (강제 불량 체크박스가 활성화 상태입니다 - 모든 웨이퍼가 NG 처리됩니다!)`);
    } else {
        addLog('sys', `Yield Control: Defect Rate = ${state.defectRate}% (불량률 ${state.defectRate}% 적용 중). Expected yield = ${(100 - state.defectRate).toFixed(1)}%`);
    }
    
    // Start loop
    startSimulationLoop();
}

function resetStopButton() {
    btnStop.textContent = "Stop (정지)";
    btnStop.style.borderColor = "";
    btnStop.style.color = "";
    btnStop.style.background = "";
}

function stopSimulation() {
    if (state.status === 'RUNNING' || state.status === 'WARNING') {
        clearInterval(state.simulationInterval);
        updateStatus('IDLE');
        addLog('sys', "Process paused by user. Plasma extinguished (사용자에 의해 공정이 일시정지되었습니다. 플라즈마 소멸).");
        
        // Restore 3D visualizer
        chamberCanvas.style.display = 'block';
        if (chamberOverlayHtml) chamberOverlayHtml.style.display = 'block';
        if (telemetryOverlay2d) telemetryOverlay2d.style.display = 'none';
        
        // Unlock inputs for configuration changes
        [inputPressure, inputTemp, inputRF, inputGasFlow, inputNH3Flow, inputN2Flow, inputProcessTime].forEach(s => s.disabled = false);
        btnStart.disabled = false;
        btnStop.disabled = true;
        btnFault.disabled = true;
        btnSave.disabled = false;
        btnReset.disabled = false;
        resetStopButton();
    } else if (state.status === 'UNLOADING' || state.status === 'LOADING') {
        // Queue a stop
        state.stopReserved = true;
        btnStop.textContent = "Stop (Reserved / 정지 예약됨)";
        btnStop.style.borderColor = "var(--color-yellow, #f59e0b)";
        btnStop.style.color = "var(--color-yellow, #f59e0b)";
        btnStop.style.background = "rgba(245, 158, 11, 0.1)";
        addLog('warn', "Stop reservation queued. Process will pause after wafer transfer completes (정지 예약 설정: 현재 웨이퍼 이송이 완료된 후 공정이 정지됩니다).");
    }
}

function resetSystem() {
    clearInterval(state.simulationInterval);
    if (state.transferTimeoutId) clearTimeout(state.transferTimeoutId); // Clear wafer transfer timers
    state.elapsedTime = 0;
    state.thickness = 0.0;
    state.uniformity = 0.0;
    state.depositionRate = 0.0;
    state.chartData = [];
    state.logHistory = [];
    state.faultActive = false;
    
    // Restore 3D visualizer
    chamberCanvas.style.display = 'block';
    if (chamberOverlayHtml) chamberOverlayHtml.style.display = 'block';
    if (telemetryOverlay2d) telemetryOverlay2d.style.display = 'none';
    
    // Reset Wafer Transfer variables
    state.waferId = 1;
    displayWaferId.textContent = 'W-01';
    state.waferOffsetX = 0;
    state.waferCycleState = 'READY';
    
    // Reset session yield stats
    state.sessionOk = 0;
    state.sessionNg = 0;
    state._isAutoRun = false;
    
    // Clear force defect checkbox
    chkForceDefect.checked = false;
    
    // Reset defect rate from input field (prevent stale high values)
    const resetRate = parseFloat(inputDefectRate.value);
    if (!isNaN(resetRate) && resetRate >= 0 && resetRate <= 100) {
        state.defectRate = resetRate;
    } else {
        state.defectRate = 10; // fallback default
        inputDefectRate.value = 10;
    }
    statusDefectRate.textContent = `Applied Defect Rate (적용 불량률): ${state.defectRate}%`;
    
    // Clear stop reservation
    state.stopReserved = false;
    resetStopButton();
    
    // Unlock sliders
    [inputPressure, inputTemp, inputRF, inputGasFlow, inputNH3Flow, inputN2Flow, inputProcessTime].forEach(s => s.disabled = false);
    
    // Fallback to SiN if current recipe state is invalid
    const recipeToLoad = (state.recipe === 'custom' || recipes[state.recipe]) ? state.recipe : 'SiN';
    applyRecipe(recipeToLoad); 
    
    updateStatus('IDLE');
    cardResult.setAttribute('data-result', '-');
    metricResult.textContent = '-';
    
    // Clear display console
    logTerminal.innerHTML = `<div class="log-entry"><span class="log-time">[${new Date().toLocaleTimeString()}]</span><span class="log-tag-sys">[SYS]</span> System reset completed. Standby (시스템 리셋 완료. 대기 상태).</div>`;
    
    btnStart.disabled = false;
    btnStop.disabled = true;
    btnFault.disabled = true;
    btnSave.disabled = true;
    
    drawCharts();
}


function simulationTick(shouldDraw = true) {
    state.elapsedTime++;
    
    // Anomaly simulation scenario matching slide 5 (if Fault Inject is clicked)
    if (state.faultActive) {
        state.faultTimer++;
        if (state.faultTimer === 2) {
            // WARNING Stage: Gas drop to 75 sccm
            inputGasFlow.value = 75;
            readInputs();
            updateStatus('WARNING');
            addLog('warn', "SiH₄ MFC Sensor reads gas starvation: Flow dropped to 75 sccm! (실란 가스 결핍 감지: 유량이 75 sccm으로 저하되었습니다!)");
            addLog('warn', "Plasma reaction fluctuating. Thickness rate slowing (플라즈마 반응 불안정. 증착 속도가 저하됩니다).");
        } else if (state.faultTimer === 8) {
            // FAULT Stage: Pressure spikes to 1.4 Torr
            inputPressure.value = 1.4;
            readInputs();
            triggerFault("Chamber throttle valve failure: Pressure spike to 1.4 Torr! (챔버 스로틀 밸브 오동작: 압력이 1.4 Torr로 급변했습니다!)");
            return; // Exit loop, stopped
        }
    }
    
    // Dynamic calculation variables
    const currentTargetThickness = calculateTotalThickness();
    const finalUniformity = calculateUniformity();
    
    // Cumulative thickness growth based on current parameter states (transient integration)
    // Only deposit when plasma is ON (after 2 seconds)
    let drInstant = 0;
    if (state.elapsedTime >= 3) {
        drInstant = currentTargetThickness / (state.processTime - 2); // nm per second, accounting for 2-sec pre-plasma phase
        state.thickness += drInstant;
    }
    
    // Set dynamic rate display (nm/min) based on current parameters
    state.depositionRate = drInstant * 60;
    
    // Uniformity can fluctuate slightly (process thermal noise)
    const noise = (Math.random() - 0.5) * 0.4;
    state.uniformity = finalUniformity + (state.status === 'WARNING' ? (Math.random() - 0.5) * 1.5 : noise);
    state.uniformity = Math.max(0, Math.min(100, state.uniformity));
    
    // UI Display Cards Update
    metricThickness.textContent = state.thickness.toFixed(1);
    metricUniformity.textContent = state.uniformity.toFixed(1);
    metricDepRate.textContent = state.depositionRate.toFixed(1);
    
    // Log tick events
    if (state.elapsedTime === 1) {
        addLog('sys', "Gas stabilization: Completed. Wafer preheating active (가스 유량 안정화 완료. 웨이퍼 예열 중).");
    } else if (state.elapsedTime === 3) {
        addLog('sys', `RF power striker: Plasma excited (플라즈마 인가 성공). [Power (출력): ${state.rfPower}W]`);
        overlayPlasma.textContent = "ON (RF ACTIVE / 플라즈마 온)";
        overlayPlasma.style.color = "var(--color-cyan)";
    } else if (state.elapsedTime % 10 === 0 && state.elapsedTime < state.processTime) {
        addLog('dep', `Deposition active (증착 진행 중). Thickness: ${state.thickness.toFixed(1)} nm / Rate: ${state.depositionRate.toFixed(1)} nm/min`);
    }
    
    state.chartData.push({ time: state.elapsedTime, thickness: state.thickness, uniformity: state.uniformity });
    
    // Draw realtime charts if requested
    if (shouldDraw) {
        drawCharts();
    }
    
    // Complete cycle validation
    if (state.elapsedTime >= state.processTime) {
        completeSequence();
    }
}

// Complete Simulation Normally and Trigger Wafer Transfer
function completeSequence() {
    clearInterval(state.simulationInterval);
    overlayPlasma.textContent = "OFF";
    overlayPlasma.style.color = "#64748b";
    
    addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Deposition target reached. Power ramping down (목표 두께 도달. 플라즈마 출력 하강).`);
    addLog('sys', "Purging chamber with N₂ carrier gas (N₂ 캐리어 가스로 챔버 퍼지 진행 중)...");
    
    // 1. 실제 장비 센서 측정값 및 물리 모델 노이즈 적용 (설정값 → 측정값 변환)
    const measured = generateMeasuredParams({
        pressure: state.pressure,
        temp: state.temp,
        rfPower: state.rfPower,
        gasFlow: state.gasFlow,
        nh3Flow: state.nh3Flow,
        n2Flow: state.n2Flow,
        processTime: state.processTime
    });

    state.thickness   = measured.thickness;
    state.uniformity  = measured.uniformity;
    state.depositionRate = measured.deposition_rate;

    // Quality evaluation checks (Physical Limits)
    let passUniformity = state.uniformity >= 95.0;
    let passThickness = state.thickness >= 480.0 && state.thickness <= 560.0;
    
    // Check manual Force Defect override first
    const isManualDefect = chkForceDefect.checked;
    
    if (isManualDefect) {
        // Force FAIL (NG) manually
        passUniformity = Math.random() > 0.5;
        passThickness = !passUniformity; // Ensure at least one fails
        state.result = 'NG';
        
        if (!passThickness) {
            state.thickness = 460.0 - Math.random() * 20.0;
        }
        if (!passUniformity) {
            state.uniformity = 92.0 - Math.random() * 4.0;
        }
        addLog('warn', `[Wafer W-${String(state.waferId).padStart(2, '0')}] [Force Defect] Forced to FAIL by manual override (체크박스 강제 불량 개입).`);
    } else {
        // Evaluate purely on physical simulation limits
        state.result = (passUniformity && passThickness) ? 'OK' : 'NG';
        if (state.result === 'OK') {
            addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] [Physics] PASS triggered by stable process parameters (정상 공정 스펙 OK).`);
        } else {
            addLog('warn', `[Wafer W-${String(state.waferId).padStart(2, '0')}] [Physics] FAIL triggered by out-of-spec parameters (스펙 이탈 NG).`);
        }
    }
    
    // Update session yield counters
    if (state.result === 'OK') {
        state.sessionOk++;
    } else {
        state.sessionNg++;
    }
    const sessionTotal = state.sessionOk + state.sessionNg;
    const actualDefectRate = sessionTotal > 0 ? ((state.sessionNg / sessionTotal) * 100).toFixed(1) : '0.0';
    
    // Update metric card elements to show the adjusted values
    metricThickness.textContent = state.thickness.toFixed(1);
    metricUniformity.textContent = state.uniformity.toFixed(1);
    metricDepRate.textContent = state.depositionRate.toFixed(1);
    
    if (state.result === 'OK') {
        addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Quality Control Check: PASS (품질 검사: 합격 - Result = OK) | Session: ${state.sessionOk}OK/${state.sessionNg}NG = 실제불량률 ${actualDefectRate}%`);
    } else {
        addLog('warn', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Quality Control Check: FAIL (품질 검사: 불합격 - Result = NG). ${!passThickness ? 'Thickness out of spec (두께 기준 이탈). ' : ''}${!passUniformity ? 'Uniformity below 95% (균일도 95% 미만).' : ''} | Session: ${state.sessionOk}OK/${state.sessionNg}NG = 실제불량률 ${actualDefectRate}%`);
    }
    
    // Record final summary log snapshot for the completed wafer (1 row per wafer)
    const finalSnapshot = {
        timestamp: getTimestamp(),
        equipment_id: 'PECVD-01',
        wafer_id: `W-${String(state.waferId).padStart(2, '0')}`,
        pressure:        measured.pressure,
        temp:            measured.temp,
        rf_power:        measured.rf_power,
        gas_flow:        measured.gas_flow,
        thickness:       parseFloat(state.thickness.toFixed(2)),
        uniformity:      parseFloat(state.uniformity.toFixed(2)),
        deposition_rate: parseFloat(state.depositionRate.toFixed(2)),
        status: 'COMPLETE',
        result: state.result
    };
    state.logHistory.push(finalSnapshot);
    
    updateStatus('COMPLETE');
    addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Chamber safety purge complete. Initiating transfer cycle.`);
    
    // Disable controls during transfer, except Stop which is used for Stop Reservation (정지 예약)
    btnStart.disabled = true;
    btnStop.disabled = false; // Allow Stop Reservation during transfer phase
    btnFault.disabled = true;
    btnSave.disabled = true;
    btnReset.disabled = true;
    
    // Trigger Wafer Unload phase scaled by speed (purge wait)
    const speedVal = parseFloat(selectSpeed.value);
    state.transferTimeoutId = setTimeout(startWaferUnload, Math.max(1, 2000 / speedVal));
}

function startWaferUnload() {
    state.waferCycleState = 'UNLOADING';
    updateStatus('UNLOADING');
    state.waferOffsetX = 0; // Start unloading from center
    addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Unloading wafer from susceptor... (웨이퍼 반출 중...)`);
    
    const speedVal = parseFloat(selectSpeed.value);
    state.transferTimeoutId = setTimeout(startWaferLoad, Math.max(1, 2500 / speedVal)); // Wait 2.5s (scaled) for slide out
}

function startWaferLoad() {
    state.waferCycleState = 'LOADING';
    updateStatus('LOADING');
    state.waferOffsetX = -400; // Slide in from far left
    
    // Increment wafer ID
    state.waferId++;
    displayWaferId.textContent = 'W-' + String(state.waferId).padStart(2, '0');
    
    // Reset active metrics
    state.thickness = 0.0;
    state.elapsedTime = 0;
    state.chartData = [];
    drawCharts();
    
    // Keep user's force defect checkbox state intact between wafer runs (do not auto-clear)
    
    metricThickness.textContent = '0.0';
    metricUniformity.textContent = '0.0';
    metricDepRate.textContent = '0.0';
    
    addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Loading fresh wafer to susceptor... (새 웨이퍼 반입 중...)`);
    
    const speedVal = parseFloat(selectSpeed.value);
    state.transferTimeoutId = setTimeout(completeWaferTransfer, Math.max(1, 2500 / speedVal)); // Wait 2.5s (scaled) for slide in
}

function completeWaferTransfer() {
    state.waferCycleState = 'READY';
    state.waferOffsetX = 0;
    updateStatus('IDLE');
    addLog('sys', `[Wafer W-${String(state.waferId).padStart(2, '0')}] Wafer aligned on heater susceptor. Ready (웨이퍼 정렬 완료. 대기 상태).`);
    
    if (state.stopReserved) {
        state.stopReserved = false;
        resetStopButton();
        addLog('sys', "[System Paused] Auto-Run paused by user reservation (정지 예약에 의해 자동 연속 공정이 일시정지되었습니다).");
        
        // Unlock inputs for configuration changes
        [inputPressure, inputTemp, inputRF, inputGasFlow, inputNH3Flow, inputN2Flow, inputProcessTime].forEach(s => s.disabled = false);
        btnStart.disabled = false;
        btnStop.disabled = true;
        btnFault.disabled = true;
        btnSave.disabled = false;
        btnReset.disabled = false;
    } else if (chkAutoRun.checked) {
        addLog('sys', `[Auto-Run] Automatically starting next deposition sequence for W-${String(state.waferId).padStart(2, '0')}...`);
        state._isAutoRun = true; // Mark as auto-run so logHistory is NOT cleared
        startSimulation();
    } else {
        // Unlock inputs for configuration changes
        [inputPressure, inputTemp, inputRF, inputGasFlow, inputNH3Flow, inputN2Flow, inputProcessTime].forEach(s => s.disabled = false);
        
        btnStart.disabled = false;
        btnStop.disabled = true;
        btnFault.disabled = true;
        btnSave.disabled = false;
        btnReset.disabled = false;
    }
}

// Inject Faults
function injectFault() {
    if (state.status !== 'RUNNING') return;
    
    state.faultActive = true;
    state.faultTimer = 0;
    addLog('warn', "External fault injection sequence loaded into controller (외부 이상 주입 시퀀스가 제어기에 로드되었습니다)...");
}

btnFault.addEventListener('click', injectFault);

// Handle Fault/Emergency Interventions
function triggerFault(reason) {
    clearInterval(state.simulationInterval);
    overlayPlasma.textContent = "OFF";
    overlayPlasma.style.color = "#64748b";
    
    updateStatus('FAULT');
    state.result = 'NG';
    // Record final summary log snapshot for the faulted wafer (1 row per wafer)
    // 고장 발생 시에도 센서 측정 노이즈를 반영한 실제 측정값 기록
    const faultMeasured = generateMeasuredParams({
        pressure: state.pressure,
        temp: state.temp,
        rfPower: state.rfPower,
        gasFlow: state.gasFlow,
        processTime: state.processTime
    });
    const faultSnapshot = {
        timestamp: getTimestamp(),
        equipment_id: 'PECVD-01',
        wafer_id: `W-${String(state.waferId).padStart(2, '0')}`,
        pressure:        faultMeasured.pressure,
        temp:            faultMeasured.temp,
        rf_power:        faultMeasured.rf_power,
        gas_flow:        faultMeasured.gas_flow,
        thickness: parseFloat(state.thickness.toFixed(2)),
        uniformity: parseFloat(state.uniformity.toFixed(2)),
        deposition_rate: parseFloat(state.depositionRate.toFixed(2)),
        status: 'FAULT',
        result: 'NG'
    };
    state.logHistory.push(faultSnapshot);
    
    addLog('err', `EMERGENCY STOP INTERVENTION (비상 정지 개입): ${reason}`);
    addLog('err', "RF Generator shutoff. MFC Gas supply cut. Chamber safety purge engaged (RF 파워 차단. 가스 밸브 잠금. 챔버 퍼지 구동).");
    
    btnStart.disabled = true;
    btnStop.disabled = true;
    btnFault.disabled = true;
    btnSave.disabled = false;
    btnReset.disabled = false;
    
    // Visual alert flash on screen
    document.body.style.animation = 'danger-flash 0.3s 3';
    setTimeout(() => { document.body.style.animation = 'none'; }, 1000);
}

function processOneWaferInstant() {
    const health = readInputs();
    
    // 1. 노이즈가 적용된 측정값 생성 (설정값 기반 물리 시뮬레이션)
    const batchMeasured = generateMeasuredParams({
        pressure: state.pressure,
        temp: state.temp,
        rfPower: state.rfPower,
        gasFlow: state.gasFlow,
        nh3Flow: state.nh3Flow,
        n2Flow: state.n2Flow,
        processTime: state.processTime
    });

    state.thickness      = batchMeasured.thickness;
    state.uniformity     = batchMeasured.uniformity;
    state.depositionRate = batchMeasured.deposition_rate;
    
    // 2. 물리적 한계점 검사 (OK / NG)
    let passUniformity = state.uniformity >= 95.0;
    let passThickness = state.thickness >= 480.0 && state.thickness <= 560.0;
    
    // Check manual Force Defect override first
    const isManualDefect = chkForceDefect.checked;
    
    let finalResult;
    
    if (isManualDefect) {
        passUniformity = Math.random() > 0.5;
        passThickness = !passUniformity;
        finalResult = 'NG';
        if (!passThickness) state.thickness = 460.0 - Math.random() * 20.0;
        if (!passUniformity) state.uniformity = 92.0 - Math.random() * 4.0;
    } else {
        // Evaluate purely on physical simulation limits
        finalResult = (passUniformity && passThickness) ? 'OK' : 'NG';
    }
    
    // Save to state variables so UI reads correct final values
    state.result = finalResult;
    
    // Record final summary log snapshot for the completed wafer (1 row per wafer)
    // 배치모드(10000x)에서도 센서 측정 노이즈를 반영한 파라미터 기록
    const batchSnapshotMeasured = generateMeasuredParams({
        pressure: state.pressure,
        temp: state.temp,
        rfPower: state.rfPower,
        gasFlow: state.gasFlow,
        processTime: state.processTime
    });
    const finalSnapshot = {
        timestamp: getTimestamp(),
        equipment_id: 'PECVD-01',
        wafer_id: `W-${String(state.waferId).padStart(2, '0')}`,
        pressure:        batchSnapshotMeasured.pressure,
        temp:            batchSnapshotMeasured.temp,
        rf_power:        batchSnapshotMeasured.rf_power,
        gas_flow:        batchSnapshotMeasured.gas_flow,
        thickness: parseFloat(state.thickness.toFixed(2)),
        uniformity: parseFloat(state.uniformity.toFixed(2)),
        deposition_rate: parseFloat(state.depositionRate.toFixed(2)),
        status: 'COMPLETE',
        result: state.result
    };
    state.logHistory.push(finalSnapshot);
    
    // Increment wafer ID
    state.waferId++;
    
    return finalResult;
}

// Alarm Flash and Hologram Animation injection
const styleSheet = document.createElement("style");
styleSheet.innerText = `
@keyframes danger-flash {
    0% { background-color: var(--bg-color); }
    50% { background-color: rgba(255, 23, 68, 0.2); }
    100% { background-color: var(--bg-color); }
}
@keyframes spin-holo {
    from { transform: rotate(0deg); }
    to { transform: rotate(360deg); }
}
@keyframes spin-holo-reverse {
    from { transform: rotate(360deg); }
    to { transform: rotate(0deg); }
}
@keyframes heartbeat {
    0%, 100% { transform: scale(1); }
    50% { transform: scale(1.08); opacity: 0.8; }
}
@keyframes scan-line {
    0% { top: 0%; }
    50% { top: 100%; }
    100% { top: 0%; }
}`;
document.head.appendChild(styleSheet);

// Helpers
function getTimestamp() {
    const now = new Date();
    return `${now.getFullYear()}-${String(now.getMonth() + 1).padStart(2, '0')}-${String(now.getDate()).padStart(2, '0')} ${String(now.getHours()).padStart(2, '0')}:${String(now.getMinutes()).padStart(2, '0')}:${String(now.getSeconds()).padStart(2, '0')}`;
}

/**
 * generateMeasuredParams() — 실제 PECVD 장비의 센서 측정 노이즈를 시뮬레이션합니다.
 *
 * 설정(레시피) 값에서 현실적인 오차를 더해 "측정값"을 반환합니다.
 * 각 파라미터의 노이즈 범위는 실제 반도체 장비의 공정 변동 데이터를 기반으로 설정되었습니다.
 *
 * 노이즈 모델:
 *   - 챔버 압력   : ±1.5% (MFC + 스로틀 밸브 응답 오차)
 *   - 히터 온도   : ±2~3℃ (PID 제어 + 열전대 측정 오차)
 *   - 플라즈마 출력: ±5~8W (RF 매칭 네트워크 임피던스 변동)
 *   - 실란 유량   : ±1~2% (MFC 유량 제어기 정밀도)
 *
 * 노이즈가 결과값에 미치는 물리적 영향 (PECVD 공정 모델):
 *   - 두께(thickness): 온도↑ → 반응속도↑, RF출력↑ → 이온 에너지↑, 유량↑ → 전구체 공급↑
 *   - 균일도(uniformity): 압력 변동이 가장 크게 영향, 압력이 설정값에 가까울수록 균일도 향상
 *   - 증착속도(dep_rate): 두께 / 공정시간으로 계산
 *
 * @param {object} setpoint - { pressure, temp, rfPower, gasFlow, processTime } 레시피 설정값
 * @returns {object} measured - 노이즈가 적용된 측정값 및 결과 지표
 */
function generateMeasuredParams(setpoint) {
    // === 1. 각 파라미터에 현실적인 가우시안 노이즈 적용 ===
    function randn() {
        let u = 0, v = 0;
        while (u === 0) u = Math.random();
        while (v === 0) v = Math.random();
        return Math.sqrt(-2.0 * Math.log(u)) * Math.cos(2.0 * Math.PI * v);
    }

    const pressureNoise = setpoint.pressure * 0.015 * randn();
    const measPressure  = parseFloat((setpoint.pressure + pressureNoise).toFixed(3));

    const tempNoise = 1.8 * randn();
    const measTemp  = parseFloat((setpoint.temp + tempNoise).toFixed(1));

    const rfNoise  = 4.0 * randn();
    const measRF   = parseFloat((setpoint.rfPower + rfNoise).toFixed(1));

    const gasNoise  = setpoint.gasFlow * 0.012 * randn();
    const measGas   = parseFloat((setpoint.gasFlow + gasNoise).toFixed(2));
    
    // Add noise for NH3 and N2 if they exist in setpoint (fallback to state if missing)
    const setNH3 = setpoint.nh3Flow !== undefined ? setpoint.nh3Flow : state.nh3Flow;
    const setN2 = setpoint.n2Flow !== undefined ? setpoint.n2Flow : state.n2Flow;
    const measNH3 = parseFloat((setNH3 + setNH3 * 0.012 * randn()).toFixed(2));
    const measN2 = parseFloat((setN2 + setN2 * 0.012 * randn()).toFixed(2));
    
    const measTime = setpoint.processTime;

    // === 2. 물리 모델 계산 (영향도 가중치 반영) ===
    let rate = 250.0;
    rate += (measTemp - 400) * 0.6;
    rate += (measRF - 300) * 0.4;
    rate += (measPressure - 1.0) * 20.0;
    rate += (measGas - 100) * 0.6;
    rate += (measNH3 - 150) * 0.05;
    rate += (measN2 - 500) * 0.01;
    rate += (measTime - 120) * -0.1;
    rate = Math.max(10, rate);

    const thicknessPhysical = rate * (measTime / 60.0);

    let uniformityPhysical = 98.0;
    uniformityPhysical -= Math.abs(measPressure - 1.0) * 15.0;
    uniformityPhysical -= Math.abs(measN2 - 500) * 0.005;
    uniformityPhysical -= Math.abs(measTemp - 400) * 0.03;
    uniformityPhysical -= Math.abs(measNH3 - 150) * 0.01;
    uniformityPhysical -= Math.abs(measRF - 300) * 0.01;
    uniformityPhysical -= Math.abs(measGas - 100) * 0.02;
    uniformityPhysical -= Math.abs(measTime - 120) * 0.005;

    // === 3. 측정값 범위 클램핑 (물리적 하한/상한) ===
    const finalThickness   = Math.max(0, thicknessPhysical);
    const finalUniformity  = Math.max(0, Math.min(100, uniformityPhysical));
    const finalDepRate     = rate;

    return {
        pressure:        measPressure,
        temp:            measTemp,
        rf_power:        measRF,
        gas_flow:        measGas,
        nh3_flow:        measNH3,
        n2_flow:         measN2,
        thickness:       parseFloat(finalThickness.toFixed(2)),
        uniformity:      parseFloat(finalUniformity.toFixed(2)),
        deposition_rate: parseFloat(finalDepRate.toFixed(2))
    };
}

// CSV Export Generator
function saveLogToCSV() {
    if (state.logHistory.length === 0) return;
    
    let csvContent = "data:text/csv;charset=utf-8,\uFEFF";
    
    // Header (Bilingual)
    const headers = [
        "timestamp (시간)", 
        "equipment_id (장비 ID)", 
        "wafer_id (웨이퍼 ID)", 
        "pressure (Torr) (챔버 압력)", 
        "temp (℃) (히터 온도)", 
        "rf_power (W) (플라즈마 출력)", 
        "gas_flow (sccm) (실란 유량)", 
        "thickness (nm) (박막 두께) [Spec: 480 ~ 560]", 
        "uniformity (%) (막 균일도) [Spec: >= 95.0]", 
        "deposition_rate (nm/min) (증착 속도)", 
        "status (장비 상태)", 
        "result (최종 판정) [Spec: Thickness 480~560 & Uniformity >=95.0]"
    ];
    csvContent += headers.join(",") + "\r\n";
    
    // Rows — only COMPLETE status wafers for yield calculation
    let totalComplete = 0;
    let totalOk = 0;
    let totalNg = 0;
    
    state.logHistory.forEach(row => {
        // Map status to bilingual
        let statusBilingual = row.status;
        if (row.status === 'IDLE') statusBilingual = "IDLE (대기)";
        else if (row.status === 'RUNNING') statusBilingual = "RUNNING (진행)";
        else if (row.status === 'WARNING') statusBilingual = "WARNING (주의)";
        else if (row.status === 'FAULT') statusBilingual = "FAULT (고장)";
        else if (row.status === 'COMPLETE') statusBilingual = "COMPLETE (완료)";
        else if (row.status === 'UNLOADING') statusBilingual = "UNLOADING (반출)";
        else if (row.status === 'LOADING') statusBilingual = "LOADING (반입)";
        
        // Map result to bilingual
        let resultBilingual = row.result;
        if (row.result === 'OK') resultBilingual = "OK (합격)";
        else if (row.result === 'NG') resultBilingual = "NG (불합격)";
        else if (row.result === '-') resultBilingual = "-";
        
        // Track yield stats for COMPLETE wafers only
        if (row.status === 'COMPLETE') {
            totalComplete++;
            if (row.result === 'OK') totalOk++;
            else if (row.result === 'NG') totalNg++;
        }

        const line = [
            row.timestamp,
            row.equipment_id,
            row.wafer_id || `W-${String(state.waferId).padStart(2, '0')}`,
            row.pressure,
            row.temp,
            row.rf_power,
            row.gas_flow,
            row.thickness,
            row.uniformity,
            row.deposition_rate,
            `"${statusBilingual}"`,
            `"${resultBilingual}"`
        ];
        csvContent += line.join(",") + "\r\n";
    });
    
    // ── Yield Summary Footer ──────────────────────────────────────────────────
    const actualDefectPct = totalComplete > 0 ? ((totalNg / totalComplete) * 100).toFixed(2) : '0.00';
    const actualYieldPct  = totalComplete > 0 ? ((totalOk / totalComplete) * 100).toFixed(2) : '0.00';
    
    csvContent += "\r\n"; // blank line
    csvContent += `"=== YIELD SUMMARY (수율 요약) ===",,,,,,,,,,,,\r\n`;
    csvContent += `"Configured Defect Rate (설정 불량률)","${state.defectRate}%",,,,,,,,,,\r\n`;
    csvContent += `"Total Wafers Processed (총 처리 웨이퍼)","${totalComplete}",,,,,,,,,,\r\n`;
    csvContent += `"Total PASS / OK (합격)","${totalOk}",,,,,,,,,,\r\n`;
    csvContent += `"Total FAIL / NG (불합격)","${totalNg}",,,,,,,,,,\r\n`;
    csvContent += `"Actual Yield Rate (실제 수율)","${actualYieldPct}%",,,,,,,,,,\r\n`;
    csvContent += `"Actual Defect Rate (실제 불량률)","${actualDefectPct}%",,,,,,,,,,\r\n`;
    // ─────────────────────────────────────────────────────────────────────────
    
    // Trigger download
    const encodedUri = encodeURI(csvContent);
    const link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", `pecvd_equipment_log_${Date.now()}.csv`);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    
    addLog('sys', `Process log exported (공정 로그 저장 완료). Total: ${totalComplete} wafers | OK: ${totalOk} | NG: ${totalNg} | 실제불량률: ${actualDefectPct}% (설정: ${state.defectRate}%)`);
}

btnStart.addEventListener('click', startSimulation);
btnStop.addEventListener('click', stopSimulation);
btnReset.addEventListener('click', resetSystem);
btnSave.addEventListener('click', saveLogToCSV);
selectSpeed.addEventListener('change', () => {
    if (state.status === 'RUNNING' || state.status === 'WARNING') {
        startSimulationLoop();
    }
});

btnApplyDefectRate.addEventListener('click', () => {
    let rate = parseFloat(inputDefectRate.value);
    if (isNaN(rate) || rate < 0 || rate > 100) {
        alert("불량률은 0%에서 100% 사이의 숫자여야 합니다.");
        return;
    }
    state.defectRate = rate;
    statusDefectRate.textContent = `Applied Defect Rate (적용 불량률): ${rate}%`;
    addLog('sys', `Defect Rate updated to ${rate}% (공정 불량률이 ${rate}%로 변경 및 적용되었습니다).`);
});

// --- Drawing 3D WebGL Chamber Animations (Three.js) ---
function init3DChamber() {
    const w = chamberCanvas.clientWidth;
    const h = chamberCanvas.clientHeight;
    
    // WebGL Renderer configuration
    renderer = new THREE.WebGLRenderer({
        canvas: chamberCanvas,
        antialias: true,
        alpha: true // Blend with CSS themes
    });
    renderer.setPixelRatio(window.devicePixelRatio);
    renderer.setSize(w, h, false);
    renderer.shadowMap.enabled = true;
    
    // Scene Setup
    scene = new THREE.Scene();
    
    // Camera Setup (3D perspective)
    camera = new THREE.PerspectiveCamera(40, w / h, 0.1, 100);
    camera.position.set(0, 4.5, 9.5); // Optimal view angle
    
    // OrbitControls for interactive rotate/pan/zoom
    controls = new THREE.OrbitControls(camera, renderer.domElement);
    controls.enableDamping = true;
    controls.dampingFactor = 0.05;
    controls.maxPolarAngle = Math.PI / 2 - 0.02; // Restrict looking under susceptor
    controls.minDistance = 3.5;
    controls.maxDistance = 16;
    controls.target.set(0, -0.3, 0);
    
    // Lights Configuration
    ambientLight = new THREE.AmbientLight(0xffffff, 0.45);
    scene.add(ambientLight);
    
    dirLight = new THREE.DirectionalLight(0xffffff, 0.7);
    dirLight.position.set(6, 12, 6);
    scene.add(dirLight);
    
    // Susceptor heater thermal glow light
    heaterLight = new THREE.PointLight(0xff4400, 0, 8);
    heaterLight.position.set(0, -1.8, 0);
    scene.add(heaterLight);
    
    // Plasma discharge fluorescent light
    plasmaLight = new THREE.PointLight(0xc084fc, 0, 10);
    plasmaLight.position.set(0, 0, 0);
    scene.add(plasmaLight);
    
    // --- 3D Model Building ---
    
    // 1. Chamber Outer Housing (translucent vacuum glass envelope)
    const chamberGeom = new THREE.CylinderGeometry(3.3, 3.3, 4.8, 32, 1, true);
    const chamberMat = new THREE.MeshPhongMaterial({
        color: 0x475569,
        transparent: true,
        opacity: 0.06,
        side: THREE.DoubleSide
    });
    chamberHousing = new THREE.Mesh(chamberGeom, chamberMat);
    scene.add(chamberHousing);
    
    // Structured industrial wireframe framework
    const wireGeom = new THREE.CylinderGeometry(3.31, 3.31, 4.8, 12, 6, true);
    const wireMat = new THREE.MeshBasicMaterial({
        color: 0x475569,
        wireframe: true,
        transparent: true,
        opacity: 0.16
    });
    const chamberWire = new THREE.Mesh(wireGeom, wireMat);
    scene.add(chamberWire);
    
    // Top & Bottom Heavy Metal Flanges
    const flangeGeom = new THREE.CylinderGeometry(3.6, 3.6, 0.18, 24);
    const flangeMat = new THREE.MeshStandardMaterial({ color: 0x334155, metalness: 0.85, roughness: 0.25 });
    const topFlange = new THREE.Mesh(flangeGeom, flangeMat);
    topFlange.position.y = 2.4;
    scene.add(topFlange);
    
    const bottomFlange = topFlange.clone();
    bottomFlange.position.y = -2.4;
    scene.add(bottomFlange);
    
    // 2. Showerhead (Gas Injector Disk)
    const showerGeom = new THREE.CylinderGeometry(2.2, 2.2, 0.25, 24);
    const metalMat = new THREE.MeshStandardMaterial({
        color: 0x64748b,
        metalness: 0.9,
        roughness: 0.15
    });
    showerhead = new THREE.Mesh(showerGeom, metalMat);
    showerhead.position.y = 2.1;
    scene.add(showerhead);
    
    // Gas supply pipe
    const pipeGeom = new THREE.CylinderGeometry(0.25, 0.25, 0.6, 16);
    const pipe = new THREE.Mesh(pipeGeom, metalMat);
    pipe.position.y = 2.5;
    scene.add(pipe);
    
    // 3. Susceptor (Lower Electrode Heater Block)
    const susceptorGeom = new THREE.CylinderGeometry(2.0, 2.0, 0.35, 24);
    const darkMetalMat = new THREE.MeshStandardMaterial({
        color: 0x1e293b,
        metalness: 0.8,
        roughness: 0.35
    });
    susceptor = new THREE.Mesh(susceptorGeom, darkMetalMat);
    susceptor.position.y = -1.95;
    scene.add(susceptor);
    
    // Center support shaft
    const shaftGeom = new THREE.CylinderGeometry(0.35, 0.35, 1.4, 16);
    const shaft = new THREE.Mesh(shaftGeom, darkMetalMat);
    shaft.position.y = -2.6;
    scene.add(shaft);
    
    // Glowing Heater coils inside susceptor (turns glowing orange-red based on temperature)
    const heaterGeom = new THREE.CylinderGeometry(1.7, 1.7, 0.08, 24);
    const heaterMat = new THREE.MeshStandardMaterial({
        color: 0xff4400,
        emissive: 0xff4400,
        emissiveIntensity: 0,
        transparent: true,
        opacity: 0.95
    });
    heaterCoils = new THREE.Mesh(heaterGeom, heaterMat);
    heaterCoils.position.y = -1.9;
    scene.add(heaterCoils);
    
    // 4. Wafer (Silicon gray circular disk sitting on susceptor)
    const waferGeom = new THREE.CylinderGeometry(1.5, 1.5, 0.04, 32);
    const waferMat = new THREE.MeshStandardMaterial({
        color: 0x475569, // Silicon base gray
        roughness: 0.45,
        metalness: 0.6
    });
    wafer = new THREE.Mesh(waferGeom, waferMat);
    wafer.position.y = -1.75;
    scene.add(wafer);
    
    // 5. Deposited Film Layer (Glowing transparent film growing on wafer)
    const filmGeom = new THREE.CylinderGeometry(1.505, 1.505, 0.01, 32); // Grows dynamically on Y scale
    const filmMat = new THREE.MeshStandardMaterial({
        color: 0x00f2fe,
        emissive: 0x00f2fe,
        emissiveIntensity: 0.25,
        transparent: true,
        opacity: 0.65,
        roughness: 0.1
    });
    film = new THREE.Mesh(filmGeom, filmMat);
    film.position.y = -1.725;
    film.visible = false;
    scene.add(film);
    
    // 6. Plasma Discharge Cloud (Volumetric neon additive blending cylinder)
    const plasmaGeom = new THREE.CylinderGeometry(2.15, 1.95, 3.5, 24, 1, true);
    const plasmaMat = new THREE.MeshBasicMaterial({
        color: 0xc084fc,
        transparent: true,
        opacity: 0.0,
        side: THREE.DoubleSide,
        blending: THREE.AdditiveBlending
    });
    plasmaCloud = new THREE.Mesh(plasmaGeom, plasmaMat);
    plasmaCloud.position.y = 0.1;
    scene.add(plasmaCloud);
    
    // 7. Pre-allocate 3D Particles Pool
    particles3D = [];
    for (let i = 0; i < maxParticles; i++) {
        const pGeom = new THREE.SphereGeometry(0.035, 6, 6);
        const pMat = new THREE.MeshBasicMaterial({
            color: 0x00f2fe,
            transparent: true,
            opacity: 0.85
        });
        const pMesh = new THREE.Mesh(pGeom, pMat);
        pMesh.visible = false;
        scene.add(pMesh);
        particles3D.push({
            mesh: pMesh,
            active: false,
            vx: 0, vy: 0, vz: 0
        });
    }
}

function updateChamberAnimation(time) {
    if (!renderer || !scene) return;
    
    // Orbit Controls Damping Update
    controls.update();
    
    // Auto-rotate camera slightly when IDLE to show off 3D depth
    if (state.status === 'IDLE') {
        scene.rotation.y = time * 0.0001;
    } else {
        scene.rotation.y = 0; // Lock to baseline during runs
    }
    
    // Plasma Glow & Gas rate calculation
    if (state.status === 'RUNNING' || state.status === 'WARNING') {
        plasmaOpacity = Math.min(0.6, plasmaOpacity + 0.015);
        particleEmissionRate = Math.max(1, Math.floor(state.gasFlow / 25));
    } else {
        plasmaOpacity = Math.max(0.0, plasmaOpacity - 0.04);
        particleEmissionRate = 0;
    }
    
    // Glow updates
    if (plasmaOpacity > 0.01) {
        plasmaCloud.visible = true;
        const pulse = 0.82 + Math.sin(time / 60) * 0.18;
        plasmaCloud.material.opacity = plasmaOpacity * pulse;
        
        let pColorHex = 0xb92ff5; // SiN: Purple
        if (state.recipe === 'SiO2') {
            pColorHex = 0x93c5fd; // SiO2: Pale blue
        } else if (state.recipe === 'aSi') {
            pColorHex = 0xf43f5e; // aSi: Pink/red
        }
        plasmaCloud.material.color.setHex(pColorHex);
        plasmaLight.color.setHex(pColorHex);
        plasmaLight.intensity = plasmaOpacity * 3.5 * pulse;
        
        // Slowly spin plasma filaments for fluid gas look
        plasmaCloud.rotation.y = time * 0.001;
    } else {
        plasmaCloud.visible = false;
        plasmaLight.intensity = 0;
    }
    
    // Susceptor Heater thermal glow
    heaterPulse = Math.sin(time / 150) * 0.12 + 0.88;
    const tempFactor = (state.temp - 300) / 200; // range 0 to 1
    if (tempFactor > 0.05) {
        const glowIntensity = tempFactor * 1.4 * heaterPulse;
        heaterCoils.material.emissiveIntensity = glowIntensity;
        heaterLight.intensity = tempFactor * 2.8 * heaterPulse;
    } else {
        heaterCoils.material.emissiveIntensity = 0;
        heaterLight.intensity = 0;
    }
    
    // Wafer displacement sliding animation (unloading / loading)
    if (state.status === 'UNLOADING') {
        state.waferOffsetX = Math.min(5.5, state.waferOffsetX + 0.065); // slide right out of chamber
    } else if (state.status === 'LOADING') {
        state.waferOffsetX = Math.min(0.0, state.waferOffsetX + 0.065); // slide in from left
    } else {
        state.waferOffsetX = 0.0;
    }
    
    wafer.position.x = state.waferOffsetX;
    film.position.x = state.waferOffsetX;
    
    // Film deposition layer height growth
    if (state.thickness > 0) {
        film.visible = true;
        const maxFilmHeight = 0.15; // 3D units thickness maximum
        const currentHeight = Math.max(0.01, (state.thickness / 560) * maxFilmHeight);
        
        film.scale.y = currentHeight / 0.01;
        film.position.y = -1.725 + (currentHeight - 0.01) / 2; // Offset center to align bottom
        
        // Film color mapping based on recipe
        let filmColorHex = 0x00f2fe; // SiN: Cyan
        if (state.recipe === 'LowStressSiN') filmColorHex = 0x7c3aed; // Purple
        if (state.recipe === 'SiO2') filmColorHex = 0x10b981; // Green
        if (state.recipe === 'aSi') filmColorHex = 0xdb2777; // Pink
        
        film.material.color.setHex(filmColorHex);
        film.material.emissive.setHex(filmColorHex);
    } else {
        film.visible = false;
    }
    
    // Emit new gas particles from showerhead disk (y = 2.0)
    if (particleEmissionRate > 0 && Math.random() > 0.45) {
        for (let k = 0; k < Math.min(3, particleEmissionRate); k++) {
            // Find inactive particle
            const p = particles3D.find(pt => !pt.active);
            if (p) {
                p.active = true;
                p.mesh.visible = true;
                
                // Random spawn coordinate inside circular showerhead disk area
                const angle = Math.random() * Math.PI * 2;
                const r = Math.random() * 1.8;
                p.mesh.position.set(Math.cos(angle) * r, 1.95, Math.sin(angle) * r);
                
                // Falling velocity vector
                p.vy = -0.055 - Math.random() * 0.035;
                p.vx = (Math.random() - 0.5) * 0.012;
                p.vz = (Math.random() - 0.5) * 0.012;
                
                // Gas particle color
                const isDarkTheme = document.body.classList.contains('dark-theme');
                let pColor = 0x00f2fe; // Cyan
                if (state.recipe === 'SiO2') {
                    pColor = Math.random() > 0.4 ? (isDarkTheme ? 0xe2e8f0 : 0x475569) : 0x00f2fe;
                } else if (Math.random() > 0.5) {
                    pColor = isDarkTheme ? 0xb92ff5 : 0x7c3aed; // Purple
                }
                p.mesh.material.color.setHex(pColor);
            }
        }
    }
    
    // Update active falling particles
    particles3D.forEach(p => {
        if (p.active) {
            p.mesh.position.x += p.vx;
            p.mesh.position.y += p.vy;
            p.mesh.position.z += p.vz;
            
            // Absorb particle once it hits susceptor / wafer level (y = -1.75)
            if (p.mesh.position.y <= -1.75) {
                p.active = false;
                p.mesh.visible = false;
            }
        }
    });
    
    // Render Frame
    renderer.render(scene, camera);
    
    state.animationFrameId = requestAnimationFrame(updateChamberAnimation);
}

// Start visualizer loop
state.animationFrameId = requestAnimationFrame(updateChamberAnimation);

// --- Drawing Custom Dashboard Line Charts ---
function drawCharts() {
    const drawSingleChart = (canvas, ctx, data, valKey, label, color, minVal, maxVal, unit) => {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        
        const w = canvas.width;
        const h = canvas.height;
        const padL = 65; // Increased from 50 to prevent left edge sticking
        const padR = 25;
        const padT = 35;
        const padB = 40; // Increased from 30 to prevent bottom edge sticking
        
        const graphW = w - padL - padR;
        const graphH = h - padT - padB;
        
        // Draw grid axes - check body class for dark theme
        const isDark = document.body.classList.contains('dark-theme');
        ctx.strokeStyle = isDark ? 'rgba(255, 255, 255, 0.05)' : 'rgba(15, 23, 42, 0.06)';
        ctx.lineWidth = 1;
        
        // Horizontal grid lines (4 rows)
        for (let i = 0; i <= 4; i++) {
            const y = padT + (graphH / 4) * i;
            ctx.beginPath();
            ctx.moveTo(padL, y);
            ctx.lineTo(w - padR, y);
            ctx.stroke();
            
            // Value Labels
            const val = maxVal - ((maxVal - minVal) / 4) * i;
            ctx.fillStyle = isDark ? '#94a3b8' : '#64748b';
            ctx.font = '10px Inter';
            ctx.textAlign = 'right';
            ctx.fillText(val.toFixed(0) + (unit || ''), padL - 10, y + 3);
        }
        
        // Vertical grid lines (5 columns)
        for (let i = 0; i <= 5; i++) {
            const x = padL + (graphW / 5) * i;
            ctx.beginPath();
            ctx.moveTo(x, padT);
            ctx.lineTo(x, h - padB);
            ctx.stroke();
            
            // Time Labels (X axis)
            const timeLabel = ((state.processTime / 5) * i).toFixed(0);
            ctx.fillStyle = isDark ? '#94a3b8' : '#64748b';
            ctx.font = '10px Inter';
            ctx.textAlign = 'center';
            ctx.fillText(timeLabel + 's', x, h - padB + 15);
        }
        
        // Plot baseline axis
        ctx.strokeStyle = isDark ? 'rgba(255, 255, 255, 0.15)' : 'rgba(15, 23, 42, 0.15)';
        ctx.lineWidth = 2;
        ctx.beginPath();
        ctx.moveTo(padL, h - padB);
        ctx.lineTo(w - padR, h - padB);
        ctx.stroke();
        
        ctx.beginPath();
        ctx.moveTo(padL, padT);
        ctx.lineTo(padL, h - padB);
        ctx.stroke();
        
        // Plot Title Label inside chart
        ctx.fillStyle = isDark ? '#cbd5e1' : '#0f172a';
        ctx.font = '12px Rajdhani';
        ctx.textAlign = 'left';
        ctx.fillText(label, padL, padT - 12);
        
        // If no data to plot, exit early
        if (data.length === 0) return;
        
        // Draw trend line
        ctx.beginPath();
        
        const getX = (item) => padL + (item.time / state.processTime) * graphW;
        const getY = (item) => {
            const val = item[valKey];
            const ratio = (val - minVal) / (maxVal - minVal);
            return h - padB - ratio * graphH;
        };
        
        ctx.moveTo(getX(data[0]), getY(data[0]));
        for (let i = 1; i < data.length; i++) {
            ctx.lineTo(getX(data[i]), getY(data[i]));
        }
        
        ctx.strokeStyle = color;
        ctx.lineWidth = 3;
        ctx.shadowColor = color;
        ctx.shadowBlur = 10;
        ctx.stroke();
        
        // Clean shadow effect for grid restoration
        ctx.shadowBlur = 0;
        
        // Fill gradient area under trend line
        ctx.beginPath();
        ctx.moveTo(getX(data[0]), h - padB);
        for (let i = 0; i < data.length; i++) {
            ctx.lineTo(getX(data[i]), getY(data[i]));
        }
        ctx.lineTo(getX(data[data.length - 1]), h - padB);
        ctx.closePath();
        
        const fillGrd = ctx.createLinearGradient(0, padT, 0, h - padB);
        fillGrd.addColorStop(0, color.replace('1)', '0.15)'));
        fillGrd.addColorStop(1, color.replace('1)', '0)'));
        ctx.fillStyle = fillGrd;
        ctx.fill();
    };
    
    // Dynamic max configurations for y-axis
    const maxThicknessTarget = calculateTotalThickness();
    const yMaxThickness = Math.max(600, Math.ceil(maxThicknessTarget / 100) * 100);
    
    // Theme-dependent colors for line plots
    const isDark = document.body.classList.contains('dark-theme');
    const thickColor = isDark ? 'rgba(0, 242, 254, 1)' : 'rgba(2, 132, 199, 1)';
    const uniColor = isDark ? 'rgba(185, 47, 245, 1)' : 'rgba(124, 58, 237, 1)';
    
    drawSingleChart(thicknessChart, thCtx, state.chartData, 'thickness', 'FILM THICKNESS PROFILES (박막 두께 실시간 추세)', thickColor, 0, yMaxThickness, 'nm');
    drawSingleChart(uniformityChart, uniCtx, state.chartData, 'uniformity', 'DEPOSITION UNIFORMITY VALUES (막 균일도 실시간 추세)', uniColor, 70, 100, '%');
}

// GUI Theme Toggle Button Handler (SEMI Light/Dark standard toggle)
const themeToggleBtn = document.getElementById('themeToggleBtn');
themeToggleBtn.addEventListener('click', () => {
    document.body.classList.toggle('dark-theme');
    const isDark = document.body.classList.contains('dark-theme');
    themeToggleBtn.textContent = isDark ? "🌓 GUI Theme (테마): Dark" : "🌓 GUI Theme (테마): Normal";
    
    // Refresh canvas overlays & redraw charts for theme contrast updates
    readInputs();
    drawCharts();
});

// Initial Priming runs
init3DChamber();
readInputs();
applyRecipe('SiN');
resizeCanvases();
addLog('sys', "Gases MFC initialization check: OK.");
addLog('sys', "Susceptor resistance heater heating: Standby.");
addLog('sys', "RF power line connection check: OK.");
