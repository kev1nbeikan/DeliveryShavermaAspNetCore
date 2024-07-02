let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").configureLogging(signalR.LogLevel.Debug).build();


const recipientId = document.getElementById('recipientId').value;
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log(recipientId);
    connection.invoke("JoinChat", {RecipientId: recipientId});    
}).catch(function (err) {
    return console.error(err.toString());
});




connection.on("ReceiveMessage", function (message) {
    addMessage(message, "other");
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    let message = document.getElementById("messageInput").value;
    addMessage(message, "me");
    connection.invoke("SendMessage", recipientId, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


const addMessage = (message, sender) => {
    let msg = message.replace(/&/g, "&")
        .replace(/g/g, ">");
    const encodedMsg = msg;
    const li = document.createElement("li");
    li.classList.add("list-group-item");
    li.classList.add(sender);
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li)
    // document.getElementById("messagesList").scrollTop = document.getElementById("messagesList").scrollHeight;
} 
