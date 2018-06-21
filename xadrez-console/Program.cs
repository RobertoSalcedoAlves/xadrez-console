using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{
    
    class Program{
        
        static void Main(string[] args){
            
            #region Boas Vindas

            Console.WriteLine($"Olá {System.Environment.GetEnvironmentVariable("Username", EnvironmentVariableTarget.Process)}!");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Bom jogo");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));

            #endregion

            try
            {                
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
                Console.ReadKey(true);
                
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }               
    }
}
