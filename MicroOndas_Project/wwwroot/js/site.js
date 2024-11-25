let tempo = 0;
let potencia = 10; 
let aquecimentoEmAndamento = false;
let intervalo; 

function adicionarNumero(numero) {
    const campoTempo = document.getElementById('tempo');
    campoTempo.value = (campoTempo.value || '') + numero;
}
function carregarProgramaPreDefinido() {
    const programa = document.getElementById('programaPreDefinido').value;

    if (!programa) {
        document.getElementById('tempo').value = '';
        document.getElementById('potencia').value = '';
        return;
    }

    $.ajax({
        url: '/MicroOndas/ObterProgramaPreDefinido',
        type: 'GET',
        data: { nome: programa },
        success: function (response) {
            if (response.sucesso) {
                document.getElementById('tempo').value = response.tempo;
                document.getElementById('potencia').value = response.potencia;

                document.getElementById('status').innerText = response.mensagem;
            } else {
                alert(response.mensagem);
            }
        },
        error: function () {
            alert('Erro ao carregar o programa pré-definido.');
        }
    });
}
function iniciar() {
    const campoTempo = document.getElementById('tempo');
    const campoPotencia = document.getElementById('potencia');

    tempo = parseInt(campoTempo.value) || 0;
    potencia = parseInt(campoPotencia.value) || 10;

    if (tempo < 1 || tempo > 120) {
        alert('Informe um tempo válido entre 1 e 120 segundos.');
        return;
    }
    if (potencia < 1 || potencia > 10) {
        alert('Informe uma potência válida entre 1 e 10.');
        return;
    }

    $.ajax({
        url: '/Microondas/IniciarAquecimento',
        type: 'POST',
        data: { tempo: tempo, potencia: potencia },
        success: function (response) {
            if (response.sucesso) {
                document.getElementById('status').innerText = `Aquecendo com potência ${potencia}...`;
                document.getElementById('tempoRestante').innerText = tempo;
                aquecimentoEmAndamento = true;
                iniciarContagemRegressiva();
            } else {
                alert(response.mensagem);
            }
        },
        error: function () {
            alert('Ocorreu um erro ao comunicar com o servidor.');
        }
    });
}

function pausarCancelar() {
    if (aquecimentoEmAndamento) {
        clearInterval(intervalo);
        aquecimentoEmAndamento = false;
        document.getElementById('status').innerText = "Aquecimento pausado.";
    } else {
        limparCampos();
        document.getElementById('status').innerText = "Aquecimento cancelado.";
    }
}

function inicioRapido() {
    tempo = 30;
    potencia = 10;
    document.getElementById('status').innerText = `Início rápido: aquecendo por ${tempo} segundos com potência ${potencia}...`;
    aquecimentoEmAndamento = true;

    iniciarContagemRegressiva();
}
function iniciarContagemRegressiva() {
    clearInterval(intervalo); 
    intervalo = setInterval(() => {
        if (tempo > 0) {
            tempo--;
            document.getElementById('tempoRestante').innerText = tempo;
        } else {
            clearInterval(intervalo);
            aquecimentoEmAndamento = false;
            document.getElementById('status').innerText = "Aquecimento concluído!";
        }
    }, 1000);
}

function limparCampos() {
    clearInterval(intervalo);
    document.getElementById('tempo').value = '';
    document.getElementById('potencia').value = '';
    document.getElementById('tempoRestante').innerText = '--';
    aquecimentoEmAndamento = false;
}