document.addEventListener('DOMContentLoaded', function() {
    const phoneNumber = document.getElementById('phoneNumber');
    const description = document.getElementById('description');
    const cardNumber = document.getElementById('cardNumber');
    const expiryDate = document.getElementById('expiryDate');
    const securityCode = document.getElementById('securityCode');

    function updateFields(paymentMethod) {
        if (paymentMethod === 'visa') {
            phoneNumber.disabled = true;
            description.disabled = false;
            cardNumber.disabled = false;
            expiryDate.disabled = false;
            securityCode.disabled = false;
        } else {
            phoneNumber.disabled = false;
            description.disabled = false;
            cardNumber.disabled = true;
            expiryDate.disabled = true;
            securityCode.disabled = true;
        }
    }

    const paymentMethods = document.querySelectorAll('input[name="paymentMethod"]');
    paymentMethods.forEach(radio => {
        radio.addEventListener('change', (event) => {
            updateFields(event.target.value);
        });
    });

    // Initial state
    updateFields(document.querySelector('input[name="paymentMethod"]:checked').value);
});
