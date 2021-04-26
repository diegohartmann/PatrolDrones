import java.awt.Color;
import java.awt.Graphics2D;

public class Heroi extends Sprite {

	String name;
	int vel;
	int dano;
	int vida;
	Color cor;
	public Heroi(String name, float posX, float posY, int sizeX, int sizeY) {
		this.name = name;
		vel = 50;
		//essas variaveis estão dentro do Sprite.
		this.posX = posX;
		this.posY = posY;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		
		vida = 100;
		dano = 10;
		cor = Color.yellow;
	}
	
	@Override
	public void gameUpate(long diffTime) {

		posX += vel * diffTime / 1000.0f;
		
	}

	@Override
	public void gameRender(Graphics2D dbg) { 
		dbg.setColor(cor);
		dbg.fillRect((int)posX, (int)posY, sizeX, sizeY);
	}

}
