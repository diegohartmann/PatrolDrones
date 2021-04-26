import java.awt.Color;
import java.awt.Graphics2D;

public class Inimigo extends Sprite {

	private String nome;
	private Color cor;
	private int velocidade;
	private int vida;
	private int danoPerto;
	private int danoLonge;

	//--------
	public int stateAtual = -1;
	public int stateAtaquePerto = 0;
	public int stateAtaqueLonge = 1;
	public int stateIdle = 2;
	public int stateLevaDano = 3;
	public int stateMorto = 4;

	//---------
	private float levaDanoContador = 0; 
	private float levaDanoFrequencia = 5; 
	private float ataquePertoContador = 0;
	private float ataquePertoFrequencia = 1;
	private float ataqueLongeContator = 0;
	private float ataqueLongeFrequencia = 0.5;
	private Color corMorto;
	private Color corIdle;
	private Color corLevaDano;

	//---------
	private int distanciaParaAtaqueLonge = 160;
	private int distanciaParaCombateMelee = 5;

	//---------
	private Random fatorDanoLongeCritico;
	private float danoCriticoChance = 0.5;
	

	public Inimigo( String _nome, Color _corIdle, float posX, float posY, int sizeX, int sizeY) {
        //essas variaveis são do Inimigo.
		this.nome = _nome;
		this.corIdle = _cor;
		this.corMorto = Color.black;
		this.corLevaDano = Color.red;
		this.cor = this.corIdle;
        //essas variaveis estão dentro do Sprite.
		this.posX = posX;
		this.posY = posY;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		//--------------------------------------
		vel = 0;
		vida = 100;
		danoLonge = 5;
		danoPerto = 15;
		fatorDanoLongeCritico = new Random();
	}
	
	@Override
	private void Renderizar(Graphics2D dbg){
		dbg.setColor(this.cor);
		dbg.fillRect((int)posX, (int)posY, sizeX, sizeY);
	}

	public void ChecarEstadoAtual(Heroi _Jogador, long diffTime){
			
		if (!TemVida()){
			EstadoAtual(this.stateMorto);
			return;
		}

		if (DistanciaAteJogador() < this.distanciaParaCombateMelee){
			levaDanoContador += diffTime / 1000.0f; 
			ataquePertoContador += diffTime / 1000.0f;
			
			if(levaDanoContador > levaDanoFrequencia) {
				EstadoAtual(this.stateLevaDano);
				levaDanoContador = 0;
				//return;
			} 
			if(ataquePertoContador > ataquePertoFrequencia) {
				EstadoAtual(this.stateAtaquePerto);
				ataquePertoContador = 0;
				//return;
			}
			return;
		}

		if(DistanciaAteJogador() < this.distanciaParaAtaqueLonge) {
			ataqueLongeContator += diffTime / 1000.0f;
			if(ataqueLongeContator > ataqueLongeFrequencia) {
				EstadoAtual(this.stateAtaqueLonge);
				ataqueLongeContator = 0;
				return;
			}
			heroi.vel = 40;
			return;
		} 
		EstadoAtual(this.stateIdle);
	}

	public void AplicarAcaoParaEstadoAtual(long _diffTime){
		switch (stateAtual) {
			
			case stateAtaquePerto:
				AtacarPerto();
				break;

			case stateAtaqueLonge:
				AtacarLonge();
				break;
			
			case stateIdle:
				Idle(_diffTime);
				break;

			case stateLevaDano:
				LevaDano();
				break;

			case stateMorto:
				Morrer();
				break;

			default:
				break;
		}
	}

	//  ==================== METODOS PARA CHECAR ESTADOS ==================
	
	private float DistanciaAteJogador(Heroi _Jogador){
		float dist = calDist(_Jogador.posX + _Jogador.sizeX/2, this.posX + this.sizeX/2,
		_Jogador.posY + _Jogador.sizeY / 2, this.posY + _Jogador.sizeY / 2);

		return dist;
	}

	private boolean TemVida(){
		return (this.vida > 0);
	}

	// ===================== ESTADOS =======================================

	private void AtacarPerto(Heroi _Jogador){
		_Jogador.vel = 40;
		InflingirDano(_Jogador, this.danoPerto);
	}

	private void AtacarLonge(Heroi _Jogador){
		if(fatorDanoLongeCritico.nextFloat() > danoCriticoChance) {
			InflingirDano(_Jogador, this.danoLonge * 1.75);
		}
		else{
			InflingirDano(_Jogador, this.danoLonge);
		}
	}

	private void Idle(long _diffTime){
		Mover(-1,0, _diffTime);
		this.cor = this.corIdle;
	}

	private void LevaDano(Heroi _Jogador){
		_Jogador.vel = 0;
		vida -= _Jogador.dano;
		if(vida <=0)
		{
			vida = 0;
		}
		this.cor = this.corLevaDano;
	}

	private void Morrer(){
		this.cor = this.corMorto;
	}
	
	//==================== METODOS USADOS NOS ESTADOS ======================
	
	private void Mover(float _xVel, float _yVel, long _diffTime){
		float moveSpeed = velocidade * (_diffTime / 1000.0f);
        posX += _xVel * moveSpeed;
        posY += _yVel * moveSpeed;
    }
	private void InflingirDano(Heroi _Jogador, int _dano){
		_Jogador.LevarDano(_dano);
	}

	private void EstadoAtual(int _estado){
		if(stateAtual != _estado)
		{
			stateAtual = _estado;
		}
	}
}
