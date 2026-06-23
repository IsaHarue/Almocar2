const cep = document.querySelector("#cep");
const rua = document.querySelector('#rua');
const bairro = document.querySelector('#bairro');
const cidade = document.querySelector('#cidade');
const estado = document.querySelector('#estado');
const erroCep = document.querySelector('#erroCep');
const value = cep.value.replace(/\D/g, '');

document.querySelector("form").addEventListener("submit", () => {
    cep.value = cep.value.replace(/\D/g, '');
});

cep.addEventListener('input', () => {
    cep.value = cep.value
        .replace(/\D/g, '')      // remove tudo que não é número
        .replace(/^(\d{5})(\d)/, '$1-$2') // adiciona o hífen
        .slice(0, 9);            // limita a 9 caracteres (8 números + hífen)
});

cep.addEventListener('blur', e => {
    consultaCep();
});
async function consultaCep() {

    rua.value = ""
    bairro.value = ""
    cidade.value = ""
    estado.value = ""

    const value = cep.value;

    const url = `https://viacep.com.br/ws/${value}/json/`;

    if (value.length !== 9  ) {
    erroCep.textContent = "CEP inválido.";
    return;
}

    await fetch(url)
        .then(response => response.json())
        .then(json => {

            rua.value = json.logradouro;
            bairro.value = json.bairro;
            cidade.value = json.localidade;
            estado.value = json.uf;

        })
        .catch(e => {
            erroCep.textContent = "CEP não encontrado. Por favor, verifique o número e tente novamente.";
            return;
            erroCep.textContent = "";

        }
    )

}