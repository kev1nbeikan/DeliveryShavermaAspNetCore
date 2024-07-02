let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").configureLogging(signalR.LogLevel.Debug).build();


const recipientId = document.getElementById('recipientId').value;
const messageInput = document.getElementById("messageInput");
const sendButton = document.getElementById("sendButton");
connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log(recipientId);
    connection.invoke("JoinChat", {RecipientId: recipientId}).then(
        function () {
            addMessage("Вы подключились к чату", "notification")
        }
    )
}).catch(function (err) {
    return addMessage(err.toString() + " Попробуйте обновить страницу", "notification");
});


connection.on("ReceiveMessage", function (message) {
    addMessage(message, "other");
});

function scrollToBottom() {
    const messagesList = document.getElementById("messageListContainer");
    messagesList.scrollTop = messagesList.scrollHeight;
}


messageInput.addEventListener('keyup', (event) => {
    if (event.key === 'Enter') {
        event.preventDefault();
        sendButton.click();
        messageInput.focus();
    }
});


document.getElementById("sendButton").addEventListener("click", function (event) {
    let message = document.getElementById("messageInput").value.trim();

    if (message === "") {
        return;
    }

    connection.invoke("SendMessage", recipientId, message).then(
        function () {
            addMessage(message, "me")
            messageInput.value = "";
            messageInput.focus();
        }
    ).catch(function (err) {
        return addMessage(err.toString() + " Попробуйте обновить страницу", "notification");
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
    scrollToBottom();
} 
