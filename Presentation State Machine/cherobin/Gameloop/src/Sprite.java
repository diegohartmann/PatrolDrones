import java.awt.Graphics2D;

public abstract class Sprite {

	float posX;
	float posY;

	int sizeX;
	int sizeY;

	public abstract void gameUpate(long diffTime);
	public abstract void gameRender(Graphics2D dbg);

}
