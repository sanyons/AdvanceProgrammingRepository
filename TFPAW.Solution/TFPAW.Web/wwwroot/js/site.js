document.addEventListener("DOMContentLoaded", function () {
    // Inicializa la primera pregunta cuando la página carga
    fetchQuestion();

    function fetchQuestion(answer = '') {
        fetch('/Akinator/GetQuestion', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(answer)
        })
            .then(response => response.json())
            .then(data => {
                document.getElementById('question').innerText = data.question;
            })
            .catch(error => console.error('Error:', error));
    }

    window.selectAnswer = function (answer) {
        fetchQuestion(answer);
    }
});
