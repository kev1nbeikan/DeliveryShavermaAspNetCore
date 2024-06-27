// function init() {
//     const openButton = document.getElementById("open-conformationWindow");
//     const confirmButton = document.getElementById("confirm");
//     const cancelButton = document.getElementById("cancel");
//     const conformationWindow = document.getElementById("conformationWindow");
//
//     if (openButton && confirmButton && cancelButton && conformationWindow) {
//         alert("asda")
//
//         openButton.addEventListener("click", () => {
//             conformationWindow.classList.remove("hidden");
//             document.getElementById("reason").focus();
//         });
//
//         confirmButton.addEventListener("click", () => {
//             const reasonTextArea = document.getElementById("reason")
//             const reason = reasonTextArea.value;
//             if (reason.trim() === '') {
//                 reasonTextArea.style.borderColor = "red";
//                 reasonTextArea.style.backgroundColor = "#FFF5F5";
//             } else {
//                 conformationWindow.classList.add("hidden");
//                 document.getElementById("reason").value = "";
//             }
//         });
//
//         cancelButton.addEventListener("click", () => {
//             conformationWindow.classList.add("hidden");
//         });
//     }
// }
//
// window.addEventListener('load', (event) => {
//     // Здесь проверьте, существуют ли элементы и переменные, прежде чем добавлять слушатели событий
//     const openButton = document.getElementById("open-conformationWindow");
//     const confirmButton = document.getElementById("confirm");
//     const cancelButton = document.getElementById("cancel");
//     const conformationWindow = document.getElementById("conformationWindow");
//
//     if (openButton && confirmButton && cancelButton && conformationWindow) {
//         openButton.addEventListener("click", () => {
//             conformationWindow.classList.remove("hidden");
//             document.getElementById("reason").focus();
//         });
//
//         confirmButton.addEventListener("click", () => {
//             const reasonTextArea = document.getElementById("reason")
//             const reason = reasonTextArea.value;
//             if (reason.trim() === '') {
//                 reasonTextArea.style.borderColor = "red";
//                 reasonTextArea.style.backgroundColor = "#FFF5F5";
//             } else {
//                 conformationWindow.classList.add("hidden");
//                 document.getElementById("reason").value = "";
//             }
//         });
//
//         cancelButton.addEventListener("click", () => {
//             conformationWindow.classList.add("hidden");
//         });
//     } else {
//         console.error('Не удалось найти необходимые элементы или переменные.');
//     }
// });
