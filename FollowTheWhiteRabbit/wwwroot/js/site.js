window.addNewChat = (textBoxElem, chatBoxElem, chatMessage) => {
    var para = document.createElement("p");
    var node = document.createTextNode(chatMessage);
    para.appendChild(node);
    chatBoxElem.appendChild(para);
    textBoxElem.focus();
};
