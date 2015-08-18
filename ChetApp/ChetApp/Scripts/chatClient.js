var ChatManager = function () {
    this.chatSubscribers = [];
    this.name = prompt('Please enter your name');
    this.connect();

};

ChatManager.prototype.subscribe = function (callbackFunc) {
    if (typeof callbackFunc !== 'function')
        throw "Callback function has to be a function";
    this.chatSubscribers.push(callbackFunc);
};


ChatManager.prototype.connect = function () {
    if (!$.connection.chatHub.client.message) {
        $.connection.chatHub.client.message = function (message) {
            self.chatSubscribers.forEach(function (subscriber) {
                subscriber(message);
            });
        };
    }
    var self = this;
   var promise =   $.connection.chatHub.connection.start()
    .then(function () {
        self.isConnected = true
    });
  

    return promise;

};

ChatManager.prototype.sendMessage = function (message) {
    message = this.name + ':' + message;
    if (!this.isConnected)
    {
       return this.connect().done(function()
        {
          return  $.connection.chatHub.server.message(message);
        });
    }
    else {
    return    $.connection.chatHub.server.message(message);
    }
   
};

document.addEventListener('DOMContentLoaded', function ()
{
    var chatManager = new ChatManager();
    chatManager.subscribe(function (message) {
        document.getElementById('chatsDiv').innerHTML += '<br/>'
            + message;
    });
    document.getElementById('btnSendMessage').addEventListener('click',
        function () {
            chatManager.sendMessage(document.getElementById('txtMyMessage').value);
        });
});