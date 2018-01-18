using tabuleiro;

namespace xadrez{
    class Dama : Peca{

        public Dama(Tabuleiro tab, Cor cor) : base(tab, cor){            
        }

        public override string ToString(){
            return "D";
        }
        private bool podeMover(Posicao pos) {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha--;
            }
            //ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
             while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha--;
                pos.coluna++;
            }            
            //direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
             while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.coluna++;
            }            
            //se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
             while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha++;
                pos.coluna++;
            }            
            //abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha++;                
            }            
            //so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
             while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha++;
                pos.coluna--;
            }            
            //esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.coluna--;
            }            
            //no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
             while(tab.posicaoValida(pos) && podeMover(pos)){
                mat[pos.linha, pos.coluna] = true;

                pos.linha--;
                pos.coluna--;
            }            
            return mat;
        }
    }
}