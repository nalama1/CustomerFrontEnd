
document.getElementById("cedula").addEventListener("input", function (e) {
    e.target.value = e.target.value.replace(/[^0-9]/g, '').slice(0, 10);
});


async function consultarCliente() {
    const cedula = document.getElementById("cedula").value;
    console.log("Consultando cliente con ID:", cedula);

    if (!cedula) {
        document.getElementById("resultado").innerText = "Por favor ingrese una cédula válida.";
        return;
    }
    if (cedula.length !== 10) {
        document.getElementById("resultado").innerText = "La cédula debe tener exactamente 10 dígitos.";
        return;
    }

    try {
        const response = await fetch(`/Cliente/ObtenerCliente?cedula=${cedula}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const resultadoDiv = document.getElementById("resultado");
        resultadoDiv.innerHTML = "";

        if (response.status === 404) {
            console.log("Error en la respuesta:", response.status);
            resultadoDiv.innerHTML = `
            <div class="result-card">
                <p style="color: red;">Cliente no encontrado</p>
            </div>`;
        } else if (response.ok) {
            const cliente = await response.json();
            console.log("Cliente encontrado:", cliente);
            resultadoDiv.innerHTML = `
            <div class="result-card">
                <p><strong>Nombre:</strong> ${cliente.nombre}</p>
                <p><strong>Apellido:</strong> ${cliente.apellido}</p>
            </div>`;
        } else {
            console.log("Error en la respuesta:", response.status);
            resultadoDiv.innerHTML = `
            <div class="result-card">
                <p style="color: red;">Ocurrió un error al consultar el cliente.</p>
            </div>`;
        }
    } catch (error) {
        document.getElementById("resultado").innerText = "Error de red. Por favor, intenta más tarde.";
        console.error("Error de consulta:", error);
    }
}



