using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace xadrez_console {

    class Tela {

        private static bool mostrarPosicao = false;
        private static string TipoPosicao = "Origem";

        public static void imprimirPartida(PartidaDeXadrez partida){
            imprimirTabuleiro(partida.tab, partida);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno);
            if(!partida.terminada){
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

                if(partida.xeque){
                    Console.WriteLine();
                    Console.WriteLine("### XEQUE! ###");
                }
                
                if (mostrarPosicao)
                {
                    MostrarTipoEntrada(partida);
                }
                else
                {
                    mostrarPosicao = true;
                }
                //if (partida.promocao)
                //{
                //    //PROMOÇÃO
                //}
            }
            else{
                Console.WriteLine("####### XEQUEMATE! #######");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);                
            }            
        }

        public static void imprimirPecasCapturadas(PartidaDeXadrez partida){
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            var aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        
        public static void imprimirConjunto(HashSet<Peca> conjunto){
            Console.Write("[");
            foreach(Peca x in conjunto){
                Console.Write(x + " ");
            }
            Console.Write("]");
        }
        public static void imprimirTabuleiro(Tabuleiro tab, PartidaDeXadrez partida)
        {
            Console.Clear();
            Console.WriteLine("    a b c d e f g h");
            Console.WriteLine("   ==================");
            for (var i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " | ");
                for (var j = 0; j < tab.colunas; j++)
                {
                    ImprimirPeca(tab.peca(i, j));
                }
                Console.Write(" | " + (8 - i));
                Console.WriteLine();
            }
            Console.WriteLine("   ==================");
            Console.WriteLine("    a b c d e f g h");
            
            if (mostrarPosicao)
            {
                MostrarTipoEntrada(partida);
            }
            else
            {
                mostrarPosicao = true;
            }
        }

        private static void MostrarTipoEntrada(PartidaDeXadrez partida)
        {
            if (mostrarPosicao)
            {
                Console.WriteLine();
                if (TipoPosicao == "Origem")
                {
                    Console.Write("Origem: ");
                    TipoPosicao = "Destino";
                }
                else
                {
                    Console.Write("Destino: ");
                    TipoPosicao = "Origem";
                }

                mostrarPosicao = false;
            }
            else
            {
                mostrarPosicao = true;
            }            
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,]posicoesPossiveis, PartidaDeXadrez partida) {
            Console.Clear();
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

             Console.WriteLine("    a b c d e f g h");
            Console.WriteLine("   ==================");
            for(var i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " | ");
                for(var j = 0; j < tab.colunas; j++) {                    
                    if(posicoesPossiveis[i,j]){
                        Console.BackgroundColor = fundoAlterado;
                    }else{
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i,j));     
                    Console.BackgroundColor = fundoOriginal;      
                }
                Console.Write(" | " + (8 - i));
                    Console.WriteLine();
            }
            Console.WriteLine("   ==================");
            Console.WriteLine("    a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
            
            mostrarPosicao = true;
            MostrarTipoEntrada(partida);
        }

        public static PosicaoXadrez lerPosicaoXadrez(PartidaDeXadrez partida) {
            string s = "";
            char coluna = ' ';
            int linha = 100;

            while(true)
            {
                s = Console.ReadLine();
                s = s.Trim();
                s = s.TrimStart();
                s = s.TrimEnd();
                if (s.Length == 2)
                {
                    char col = Convert.ToChar(Convert.ToString(s[0]).ToLower());
                    if (col == 'a' || col == 'b' || col == 'c' || col == 'd' || col == 'e' || col == 'f' || col == 'g' || col == 'h')
                    {
                        try
                        {
                            int lin = int.Parse(s[1].ToString());
                            if (lin > 0 && lin < 9)
                            {
                                coluna = s[0];
                                linha = int.Parse(s[1] + "");
                                if (partida.validarPosicaoDeOrigem(new PosicaoXadrez(coluna, linha).ToPosicao()))
                                {
                                    return new PosicaoXadrez(coluna, linha);
                                }
                                else
                                {
                                    TipoPosicao = "Origem";
                                }
                            }
                            else
                            {
                                Console.Clear();
                                TipoPosicao = "Origem";
                                imprimirPartida(partida);
                            }
                        }
                        catch (Exception erro)
                        {
                            Console.Write(erro.Message);
                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                            Console.Clear();
                            TipoPosicao = "Origem";
                            imprimirPartida(partida);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        TipoPosicao = "Origem";
                        imprimirPartida(partida);
                    }
                }
                else
                {
                    Console.Clear();
                    TipoPosicao = "Origem";
                    imprimirPartida(partida);
                }
            }
        }

        public static PosicaoXadrez lerPosicaoXadrez(PartidaDeXadrez partida, bool[,]posicoesPossiveis, Posicao origem)
        {
            string s = "";
            char coluna = ' ';
            int linha = 100;

            while (true)
            {
                s = Console.ReadLine();
                s = s.Trim();
                s = s.TrimStart();
                s = s.TrimEnd();
                if (s.Length == 2)
                {
                    char col = Convert.ToChar(Convert.ToString(s[0]).ToLower());
                    if (col == 'a' || col == 'b' || col == 'c' || col == 'd' || col == 'e' || col == 'f' || col == 'g' || col == 'h')
                    {
                        try
                        {
                            int lin = int.Parse(s[1].ToString());
                            if (lin > 0 && lin < 9)
                            {
                                coluna = s[0];
                                linha = int.Parse(s[1] + "");
                                if (partida.validarPosicaoDestino(origem, new PosicaoXadrez(coluna, linha).ToPosicao()))
                                {
                                    return new PosicaoXadrez(coluna, linha);
                                }
                                else
                                {
                                    TipoPosicao = "Destino";
                                }
                            }
                            else
                            {
                                Console.Clear();
                                TipoPosicao = "Destino";
                                imprimirTabuleiro(partida.tab, posicoesPossiveis, partida);
                            }
                        }
                        catch (Exception erro)
                        {
                            Console.Write(erro.Message);
                            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                            Console.Clear();
                            TipoPosicao = "Destino";
                            imprimirTabuleiro(partida.tab, posicoesPossiveis, partida);
                        }
                       
                    }
                    else
                    {
                        Console.Clear();
                        TipoPosicao = "Destino";
                        imprimirTabuleiro(partida.tab, posicoesPossiveis, partida);
                    }
                }
                else
                {
                    Console.Clear();
                    TipoPosicao = "Destino";
                    imprimirTabuleiro(partida.tab, posicoesPossiveis, partida);
                }
            }
        }

        public static void ImprimirPeca(Peca peca) {

            if(peca == null){
                Console.Write("- ");
            }else{
                if(peca.cor == Cor.Branca) {
                    Console.Write(peca);
            }   else {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }                
                    Console.Write(" ");
            }
        }
    }
}
