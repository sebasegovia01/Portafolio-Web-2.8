// Set up the canvas
var canvas = document.getElementById("sigCanvas");
var ctx = document.getElementById("sigCanvas").getContext("2d");
ctx.strokeStyle = "#222222";
ctx.lineWith = 2;

// Set up mouse events for drawing
var drawing = false;
var mousePos = { x:0, y:0 };
var lastPos = mousePos;
document.getElementById("sigCanvas").addEventListener("mousedown", function (e) {
        drawing = true;
  lastPos = getMousePos(document.getElementById("sigCanvas"), e);
}, false);
document.getElementById("sigCanvas").addEventListener("mouseup", function (e) {
  drawing = false;
}, false);
document.getElementById("sigCanvas").addEventListener("mousemove", function (e) {
  mousePos = getMousePos(document.getElementById("sigCanvas"), e);
}, false);

// Get the position of the mouse relative to the canvas
function getMousePos(canvasDom, mouseEvent) {
  var rect = canvasDom.getBoundingClientRect();
  return {
    x: mouseEvent.clientX - rect.left,
    y: mouseEvent.clientY - rect.top
  };
}

// Get a regular interval for drawing to the screen
window.requestAnimFrame = (function (callback) {
    return window.requestAnimationFrame || 
       window.webkitRequestAnimationFrame ||
       window.mozRequestAnimationFrame ||
       window.oRequestAnimationFrame ||
       window.msRequestAnimaitonFrame ||
       function (callback) {
    window.setTimeout(callback, 1000/60);
       };
})();


// Draw to the canvas
function renderCanvas() {
    if (drawing) {
      ctx.moveTo(lastPos.x, lastPos.y);
      ctx.lineTo(mousePos.x, mousePos.y);
      ctx.stroke();
      lastPos = mousePos;
    }
  }
  
  // Allow for animation
  (function drawLoop () {
    requestAnimFrame(drawLoop);
    renderCanvas();
  })();



// Set up the UI
var sigText = document.getElementById("sigDataUrl");
var sigImages = document.getElementById("sigImage");
var clearBtn = document.getElementById("sigClearBtn");
var submitBtn = document.getElementById("sigSubmitBtn");

clearBtn.addEventListener("click", function (e) {
    clearCanvas();
    sigText.innerHTML = "";
    sigImages.setAttribute("src", "");
}, false);

/*submitBtn.addEventListener("click", function (e) {
    var dataUrl = canvas.toDataURL();
    sigText.innerHTML = dataUrl;
    sigImages.setAttribute("src", dataUrl);
    return false;
    e.preventDefault();
}, false);*/


// Set up mouse events for drawing
var drawing = false;
var mousePos = { x:0, y:0 };
var lastPos = mousePos;
document.getElementById("sigCanvas").addEventListener("mousedown", function (e) {
    drawing = true;
    lastPos = getMousePos(document.getElementById("sigCanvas"), e);
}, false);
document.getElementById("sigCanvas").addEventListener("mouseup", function (e) {
    drawing = false;
}, false);
document.getElementById("sigCanvas").addEventListener("mousemove", function (e) {
    mousePos = getMousePos(document.getElementById("sigCanvas"), e);
}, false);

// Set up touch events for mobile, etc
document.getElementById("sigCanvas").addEventListener("touchstart", function (e) {
    mousePos = getTouchPos(document.getElementById("sigCanvas"), e);
    var touch = e.touches[0];
    var mouseEvent = new MouseEvent("mousedown", {
        clientX: touch.clientX,
        clientY: touch.clientY
    });
    document.getElementById("sigCanvas").dispatchEvent(mouseEvent);
}, false);
document.getElementById("sigCanvas").addEventListener("touchend", function (e) {
    var mouseEvent = new MouseEvent("mouseup", {});
    document.getElementById("sigCanvas").dispatchEvent(mouseEvent);
}, false);
document.getElementById("sigCanvas").addEventListener("touchmove", function (e) {
    var touch = e.touches[0];
    var mouseEvent = new MouseEvent("mousemove", {
        clientX: touch.clientX,
        clientY: touch.clientY
    });
    document.getElementById("sigCanvas").dispatchEvent(mouseEvent);
}, false);



  // Prevent scrolling when touching the canvas
document.body.addEventListener("touchstart", function (e) {
    if (e.target == document.getElementById("sigCanvas")) {
      e.preventDefault();
    }
  }, false);
  document.body.addEventListener("touchend", function (e) {
    if (e.target == document.getElementById("sigCanvas")) {
      e.preventDefault();
    }
  }, false);
  document.body.addEventListener("touchmove", function (e) {
    if (e.target == document.getElementById("sigCanvas")) {
      e.preventDefault();
    }
  }, false);



  function clearCanvas() {
    document.getElementById("sigCanvas").width = document.getElementById("sigCanvas").width;
}
