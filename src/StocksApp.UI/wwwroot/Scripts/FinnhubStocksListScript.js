// Create a web socket 
const token = document.querySelector("#FinnhubToken").value;
const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

var stockSymbolTag = document.querySelectorAll(".stockSymbol");
var symbolList = Array.from(stockSymbolTag, symbol => symbol.textContent);
console.log(symbolList);

// Connection opened. Subcribe to the symbols
socket.addEventListener('open', function(event) {
  for (var i = 0; i < symbolList.length; i++) {
    socket.send(JSON.stringify({ 'type': 'subscribe', 'symbol': symbolList[i] }));
  } 
});

// Listen for messages
socket.addEventListener('message', function(event) {
  //if error message is received from server
  if (event.data.type == "error") {

    return; //exit the function
  }

  //data received from server
  //console.log('Message from server ', event.data);

  /* Sample response:
  {"data":[{"p":220.89,"s":"MSFT","t":1575526691134,"v":100}],"type":"trade"}
  type: message type
  data: [ list of trades ]
  s: symbol of the company
  p: Last price
  t: UNIX milliseconds timestamp
  v: volume (number of orders)
  c: trade conditions (if any)
  */

  var eventData = JSON.parse(event.data);
  if (eventData) {
    if (eventData.data) {
      //get the updated price
      var updatedPrice = JSON.parse(event.data).data[0].p;
      var symbol = JSON.parse(event.data).data[0].s;
      //console.log(updatedPrice);

      //update the UI
      $("#" + symbol).text(updatedPrice.toFixed(2));
    }
  }
});

// Unsubscribe
var unsubscribe = function (symbol) {
  //disconnect from server
  socket.send(JSON.stringify({ 'type': 'unsubscribe', 'symbol': symbol }))
}

//when the page is being closed, unsubscribe from the WebSocket
window.onunload = function () {
  for (var i = 0; i < symbolList.length; i++) {
    unsubscribe(symbolList[i]);
  }
};