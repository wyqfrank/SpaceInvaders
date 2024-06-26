const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const msg = document.createElement('div');
    msg.textContent = `${user}: ${message}`;
    document.getElementById('messages').appendChild(msg);
});

connection.start().catch(err => console.error(err.toString()));

function sendMessage() {
    const user = document.getElementById('user').value;
    const message = document.getElementById('message').value;
    connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
}
