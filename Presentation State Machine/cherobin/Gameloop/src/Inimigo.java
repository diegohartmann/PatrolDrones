import java.awt.Color;
import java.awt.Graphics2D;

public class Inimigo extends Sprite {

	String name;
	Color color;
	int vel;
	int vida;
	int danoLonge;
	int danoPerto;
	
	public Inimigo(String name, float posX, float posY, int sizeX, int sizeY) {
		this.name = name;
	
		//essas variaveis estão dentro do Sprite.
		this.posX = posX;
		this.posY = posY;
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		
		color = Color.red;
		vel = 0;
		vida = 100;
		danoLonge = 5;
		danoPerto = 15;
	}
	
	@Override
	public void gameUpate(long diffTime) {

		posX -= vel * diffTime / 1000.0f;

	}

	@Override
	public void gameRender(Graphics2D dbg) { 
		dbg.setColor(color);
		dbg.fillRect((int)posX, (int)posY, sizeX, sizeY);
	}

}
