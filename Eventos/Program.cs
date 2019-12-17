using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos
{
    class Program
    {

        /// <summary>
        /// Publisher ou editor (ou emissor) publica uma notificação (ou informação) e o assinante recebe esta notificação. 
        /// Publisher é o tipo que contém o delegate.
        /// </summary>
        class Publisher
        {
            /// <summary>
            /// Delegate
            /// </summary>
            /// <param name="sender">referência ao objeto que disparou um evento</param>
            /// <param name="args"> EventArgs é uma classe especial que pode ser derivada para criar classes personalizadas para passar informações quando um evento é acionado</param>
            public delegate void TrabalhoFeitoEventHandler(object sender, EventArgs args);

            // Evento
            public event TrabalhoFeitoEventHandler TrabalhoFeito;

            public void ProcessaUmTrabalho()
            {
                Console.WriteLine("Publisher/Editor: Um trabalho foi concluído");
                // Disparando o evento
                OnTrabalhoFeito();
            }

            // O padrão standard requer que o método seja definido como virtual. O nome também precisa coincidir com o nome do evento e será prefixado com 'On'.
            protected virtual void OnTrabalhoFeito()
            {
                // Verifica se há pelo menos um assinante para o evento bem como se não existe nenhum assinante, pois neste caso disparar o evento causaria uma exceção.
                if (TrabalhoFeito != null)
                    // Se existem assinantes o evento é gerado como se fosse um método, passando uma referência ao objeto atual e os argumentos do evento que foram passados no parâmetro.
                    TrabalhoFeito(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Subscribers ou assinantes têm a liberdade de quando começar a ouvir e quando parar de ouvir (em termos de programação, quando se registrar e quando cancelar o registro).
        /// Subscribers ou assinantes não falam um com o outro. Na verdade, eles são os alvos principais que suportam a arquitetura de eventos.
        /// </summary>
        class Subscriber
        {
            // Tratando o evento
            public void OnTrabalhoFeitoEventHandler(object sender, EventArgs args)
            {
                Console.WriteLine("O Subscriber/Assinante foi notificado");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Teste de evento");

            // Instanciando o editor e o assinante
            Publisher remetente = new Publisher();
            Subscriber receptor = new Subscriber();

            // Definindo o tratamento do evento
            remetente.TrabalhoFeito += receptor.OnTrabalhoFeitoEventHandler;

            // Invocando o método
            remetente.ProcessaUmTrabalho();

            Console.WriteLine("\n");

            Carro carro = new Carro();
            carro.ExcedeuVelocidadeSeguranca += new VelocidadeSegurancaExcedidaEventHandler(CarroLimiteVelocidadeExcedida);

            for (int i = 0; i < 3; i++)
            {
                carro.Acelerar(30);
                Console.WriteLine("Velocidade atual: {0} Kmh", carro.Velocidade);
            }

            Console.ReadKey();
        }

        static void CarroLimiteVelocidadeExcedida(object source, EventArgs e)
        {
            Carro carro = (Carro) source;
            Console.WriteLine("O limite de velocidade foi excedido ({0}Kmh)", carro.Velocidade);
        }
    }
}
