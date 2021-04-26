import java.awt.Color;
import java.awt.Graphics2D;

public class Heroi extends Sprite {

	private String name;
	private int velocidade;
	private int dano;
	private int vida;
	private Color cor;
	public Heroi(String _name, Color _cor, float posX, float posY, int sizeX, int sizeY) {
		this.name = _name;
		this.cor = _cor;
		//essas variaveis estão dentro do Sprite.
		this.posX = posX;
		this.posY = posY;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		//------------------------------
		vel = 50;
		vida = 100;
		dano = 10;
	}
	

	@Override
	public void Renderizar(Graphics2D dbg) { 
		dbg.setColor(this.cor);
		dbg.fillRect((int)posX, (int)posY, sizeX, sizeY);
	}

	public void Mover(float _xVel, float _yVel, long _diffTime){
        posX += _xVel * velocidade * (_diffTime / 1000.0f);
        posY += _yVel * velocidade * (_diffTime / 1000.0f);
    }

	public void LevarDano(float _dano){
		vida -= _dano;
		if(vida <=0)
		{
			vida = 0;
		}
	}

}
