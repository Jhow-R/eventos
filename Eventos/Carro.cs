using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos
{
    delegate void VelocidadeSegurancaExcedidaEventHandler(object sender, EventArgs e);

    class Carro
    {
        private int velocidade = 0;
        private int velocidadeSeguranca = 70;

        public int Velocidade
        {
            get
            {
                return velocidade;
            }
        }

        public event VelocidadeSegurancaExcedidaEventHandler ExcedeuVelocidadeSeguranca;

        /// <summary>
        /// O método é marcado como virtual, ele pode ser substituído e alterado por classes que são derivadas a partir de Carro.
        /// 
        /// </summary>
        /// <param name="e">argumentos do evento para o evento gerado.</param>
        public virtual void OnVelocidadeSegurancaExcedida(EventArgs e)
        {
            // Verifica se há pelo menos um assinante para o evento bem como se não existe nenhum assinante, pois neste caso disparar o evento causaria uma exceção.
            if (ExcedeuVelocidadeSeguranca != null)
                // Se existem assinantes o evento é gerado como se fosse um método, passando uma referência ao objeto atual e os argumentos do evento que foram passados no parâmetro.
                ExcedeuVelocidadeSeguranca(this, e);
        }   

        public void Acelerar(int kmh)
        {
            int velocidadeAnterior = velocidade;
            velocidade += kmh;

            if (velocidadeAnterior <= velocidadeSeguranca && velocidade > velocidadeSeguranca)
                OnVelocidadeSegurancaExcedida(new EventArgs());
        }
    }
}
