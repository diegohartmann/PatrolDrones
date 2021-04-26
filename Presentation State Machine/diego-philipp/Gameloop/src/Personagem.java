import java.awt.Color;
import java.awt.Graphics2D;

public class Personagem extends Sprite {

    boolean player;
	String nome;
	Color cor;
	int velocidade;
	int vida;
	int danoPerto;
	int danoLonge;
	
	public Personagem(boolean _player, String _nome, Color _cor, float posX, float posY, int sizeX, int sizeY) {
        
        //essas variaveis são do Personagem.
		this.jogavel = _player;
		this.nome = _nome;
		this.cor = _cor;
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
	}
	
	@Override
	public void gameUpate(long diffTime) {
		// posX -= vel * diffTime / 1000.0f;
        Mover(2.2,)
	}

	@Override
	public void gameRender(Graphics2D dbg) { 
		dbg.setColor(color);
		dbg.fillRect((int)posX, (int)posY, sizeX, sizeY);
	}

    public void Mover(float _xVel, float _yVel, long _diffTime){
        posX += _xVel * velocidade * (_diffTime / 1000.0f);
        posY += _yVel * velocidade * (_diffTime / 1000.0f);
    }

}
