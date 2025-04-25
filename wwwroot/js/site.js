// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function consultarCep(cep) {
    return $.ajax({
        url: `https://brasilapi.com.br/api/cep/v1/${cep}`,
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log('Dados do CEP:', data);
        },
        error: function (xhr, status, error) {
            console.error('Erro ao consultar o CEP:', error);
        }
    });
}

// Função para buscar veículos por fabricante e modelo na tela de Realizar Venda
function buscarVeiculos(fabricante, modelo) {
    return $.get('/Venda/Veiculos', { fabricante: fabricante, modelo: modelo });
}

// Exemplo de uso (adicione o código abaixo no evento desejado na sua view RealizarVenda):
// buscarVeiculos('Ford', 'Fiesta').then(function(data) {
//     console.log('Veículos encontrados:', data);
// });
