using tabuleiro;

namespace xadrez{
    class Rei : Peca{

        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor){            
        }

        public override string ToString(){
            return "R";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        //poupa linhas invertendo o sinal
        private int inverteLado(){    
            if(this.cor == Cor.Branca){
                return 1;
            }else{
               return -1;
            }
        }

        private int saltoRoque(string TipoRoque_grande_pequeno){
            int salto = 0;
            if(TipoRoque_grande_pequeno == "grande"){
                salto = 2;
            }else if(TipoRoque_grande_pequeno == "pequeno"){
                salto = 1;
            }else{
                throw new TabuleiroException("Erro interno: erro na definição: TipoRoque_grande_pequeno");
            }
            return salto;
        }

         private bool temVizinho(Posicao posicao, string TipoRoque_p_g){
            Posicao pos = new Posicao(0, 0);
            int salto = 0;
            if(TipoRoque_p_g == "p"){
                salto = saltoRoque("pequeno");
                for(var i = 0; i < salto; i++){
                    pos.definirValores(posicao.linha, posicao.coluna + salto + i * inverteLado());
                    if(tab.existePeca(pos)){
                        return true;
                    }
                }
            }else if (TipoRoque_p_g == "g"){
                salto = saltoRoque("grande");
                for(var i = 0; i < salto; i++){
                    pos.definirValores(posicao.linha, posicao.coluna + salto + i * inverteLado());
                    if(tab.existePeca(pos)){
                        return true;
                    }
                }
            }else{
                throw new TabuleiroException("Erro interno: erro na definição: 'TipoRoque_p_g'");
            }
            return false;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);
           

            //RoquePequeno
            if(!temVizinho(posicao, "p") && qteMovimentos == 0 && tab.peca){
                pos.definirValores(posicao.linha, posicao.coluna + 2 * inverteLado());
                if(tab.posicaoValida(pos) && podeMover(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
            }

            //RoqueGrande
            if(!temVizinho(posicao, "p")){
                pos.definirValores(posicao.linha, posicao.coluna + 2 * inverteLado());
                if(tab.posicaoValida(pos) && podeMover(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
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