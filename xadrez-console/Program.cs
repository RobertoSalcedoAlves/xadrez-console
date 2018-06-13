using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{
    class Program{
        static void Main(string[] args){
            try {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada) {
                    try{
                        Console.Clear();
                        Tela.imprimirPartida(partida);                        
                        
                        Posicao origem = Tela.lerPosicaoXadrez(partida).ToPosicao();
                        partida.validarPosicaoDeOrigem(origem);
                        partida.mudaTipoPosicao();

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis, partida);

                        Posicao destino = Tela.lerPosicaoXadrez(partida, posicoesPossiveis, origem).ToPosicao();
                        partida.validarPosicaoDestino(origem,destino);
                        partida.mudaTipoPosicao();

                        partida.realizaJogada(origem, destino);

                    } catch (TabuleiroException e) {
                        Console.WriteLine(e.Message);
                        Console.ReadKey();
                    }                        
                }
                Console.Clear();
                Tela.imprimirPartida(partida);
                
            }catch (TabuleiroException e) {
                        Console.WriteLine(e.Message);
                    }
                    Console.ReadKey();
        }               
    }
}
