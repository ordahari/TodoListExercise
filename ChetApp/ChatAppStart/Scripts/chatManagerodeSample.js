//testing
var manager = new ChatManager()

manager.connect();

manager.subscribe(function (message) {
    console.log(message);
});

manager.sendMessage('Hello')