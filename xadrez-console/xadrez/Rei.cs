using tabuleiro;

namespace xadrez{
    class Rei : Peca{
        private PartidaDeXadrez partida;

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor){
            this.partida = partida;
        }

        public override string ToString(){
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        private bool podeRoquePequeno(){
            Posicao pos = new Posicao(0, 0);

            if(this.qteMovimentos == 0)
            {
                return
                    !temVizinho(posicao, "pequeno")
                    && qteMovimentos == 0
                    && tab.peca(posicao.linha, posicao.coluna + 3).qteMovimentos == 0
                    && !partida.xeque;
            }
            return false;            
        }

        private bool podeRoqueGrande(){
            Posicao pos = new Posicao(0, 0);

            if (this.qteMovimentos == 0)
            {
                return
                    !temVizinho(posicao, "grande")
                    && qteMovimentos == 0
                    && tab.peca(posicao.linha, posicao.coluna - 4).qteMovimentos == 0
                    && !partida.xeque;
            }
            return false;
        }

         private bool temVizinho(Posicao posicao, string tipoRoque){
            Posicao pos = new Posicao(0, 0);
            bool resultado = false;
            
            if(tipoRoque == "pequeno"){
                for(var i = 1; i < 3; i++){
                    pos.definirValores(posicao.linha, posicao.coluna + i);
                    if(tab.existePeca(pos)){
                        resultado = true;
                        break;
                    }
                }
            }else if(tipoRoque == "grande"){
                for(var i = 1; i < 4; i++){
                    pos.definirValores(posicao.linha, posicao.coluna - i);
                    if(tab.existePeca(pos)){
                        resultado = true;
                        break;
                    }
                }
            }else{
                throw new TabuleiroException("Erro interno: tipo de roque informado inválido!");
            }
            return resultado;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);           

            //RoquePequeno
            if(podeRoquePequeno()){
                pos.definirValores(posicao.linha, posicao.coluna + 2);
                mat[pos.linha, pos.coluna] = true;
            }

            //RoqueGrande
            if(podeRoqueGrande()){
                pos.definirValores(posicao.linha, posicao.coluna - 2);
                mat[pos.linha, pos.coluna] = true;
            }

            //acima
            pos.definirValores(posicao.linha -1, posicao.coluna);
            if(tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            return mat;
        }
    }
}