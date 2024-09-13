document.addEventListener('DOMContentLoaded', () => {
    const quantityInputs = document.querySelectorAll('.quantity-input');
    const totalPriceElement = document.getElementById('total-price');

    quantityInputs.forEach(input => {
        input.addEventListener('change', updateTotalPrice);
    });

    function updateTotalPrice() {
        let total = 0;
        const cartRows = document.querySelectorAll('.cart-row');

        cartRows.forEach(row => {
            const priceElement = row.querySelector('.cart-price');
            const quantityElement = row.querySelector('.quantity-input');
            const totalElement = row.querySelector('.cart-total');

            const price = parseFloat(priceElement.innerText.replace(' Ariary', ''));
            const quantity = parseInt(quantityElement.value);
            if (quantity > 10) {
                quantityElement.value = 10;
            }
            const totalForRow = price * quantity;

            totalElement.innerText = `${totalForRow} Ariary`;
            total += totalForRow;
        });

        totalPriceElement.innerText = `${total} Ariary`;
    }
});
