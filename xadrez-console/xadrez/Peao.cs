using tabuleiro;

namespace xadrez{
    class Peao : Peca{
        private PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor){
            this.partida = partida;
        }

        public override string ToString(){
            return "P";
        }

        private bool existeInimigo(Posicao pos){
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos){
            return tab.peca(pos) == null;
        }
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //poupa linhas invertendo o sinal
            int corPeca = 0;

            if(this.cor == Cor.Branca){
                corPeca = 1;
            }else{
               corPeca = -1;
            }

            //primeiroMovimento           
            pos.definirValores(posicao.linha - 2 * corPeca, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos) && qteMovimentos == 0) {
                mat[pos.linha, pos.coluna] = true;
            }

            //Avanco
            pos.definirValores(posicao.linha - 1 * corPeca, posicao.coluna);
            if (tab.posicaoValida(pos) && livre(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            //CapturaEsquerda
            pos.definirValores(posicao.linha - 1 * corPeca, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

             //CapturaDireita
            pos.definirValores(posicao.linha - 1 * corPeca, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            //if(posicao.linha == 3)
            //{
                Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                if(tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                {
                    if (this.cor == Cor.Branca)
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    else
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }                        
                }

                Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    if (this.cor == Cor.Branca)
                    {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                    else
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }

            //}
            return mat;
        }
    }
}