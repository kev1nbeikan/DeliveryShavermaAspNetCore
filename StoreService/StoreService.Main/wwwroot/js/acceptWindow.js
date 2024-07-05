const confirmButtonAccept = document.getElementById("confirmAccept");
const cancelButtonAccept = document.getElementById("cancelAccept");
const conformationWindowAccept = document.getElementById("conformationWindowAccept");
let _orderIdAccept = "";

function openConformationWindowAccept(orderId) {
    conformationWindowAccept.classList.remove("hidden");
    document.getElementById("reason").focus();
    _orderIdAccept = orderId;
}

confirmButtonAccept.addEventListener("click", () => {
    conformationWindowAccept.classList.add("hidden");
    acceptOrder();

});

cancelButtonAccept.addEventListener("click", () => {
    conformationWindowAccept.classList.add("hidden");
});

function acceptOrder() {
    fetch(`http://localhost:5106/orders/store/waitingCourier/${_orderIdAccept}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'UserId': AuthHeaders.UserId,
            'Role': AuthHeaders.Role
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Не удалось отменить заказ');
            }
            getCurrentStoreOrders();
            showSuccessMessage();
            console.log('Заказ поменял статус!', response);
        })
        .catch(error => {
            showErrorMessage("Произошла ошибка");
            console.error('Ошибка при отправке запроса на прием:', error);
        });
}