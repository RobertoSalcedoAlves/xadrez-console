using tabuleiro;

namespace xadrez{
    class Bispo : Peca{

        public Bispo(Tabuleiro tab, Cor cor) : base(tab, cor){            
        }

        public override string ToString(){
            return "B";
        }

        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while(tab.posicaoValida(pos) && podeMover(pos)){                
                mat[pos.linha, pos.coluna] = true;

                pos.linha--;
                pos.coluna--;
            }

           //ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while(tab.posicaoValida(pos) && podeMover(pos)){                
                mat[pos.linha, pos.coluna] = true;

                pos.linha--;
                pos.coluna++;
            }

            //se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while(tab.posicaoValida(pos) && podeMover(pos)){                
                mat[pos.linha, pos.coluna] = true;

                pos.linha++;
                pos.coluna++;
            }

            //so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while(tab.posicaoValida(pos) && podeMover(pos)){                
                mat[pos.linha, pos.coluna] = true;

                pos.linha++;
                pos.coluna--;
            }
            return mat;
        }
    }
}