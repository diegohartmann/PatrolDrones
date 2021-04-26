import java.awt.BorderLayout;
import java.awt.Canvas;
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics2D;
import java.awt.RenderingHints;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.image.BufferStrategy;
import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;

import javax.imageio.ImageIO;
import javax.swing.JFrame;
import javax.swing.JPanel;
 

public class GamePanel extends JPanel implements Runnable {

	private static final long serialVersionUID = 1L;

	public static GamePanel instance = null;

	public static final int PWIDTH = 640; // size of panel
	public static final int PHEIGHT = 600;
	private Thread animator; // for the animation
	private boolean running = false; // stops the animation

	private Graphics2D dbg;
	private int FPS, SFPS;
	private long CurrentSecond = 0;
	private long NewSecond = 0;
	
	private Canvas gui = null;
	private BufferStrategy strategy = null;
	private Heroi heroi;
	private Inimigo inimigo;
	private List<Inimigo> inimigosCriados = new List<Inimigo>();

 
	public static void main(String args[]) {
		GamePanel ttPanel = new GamePanel();
		// create a JFrame to hold the timer test JPanel
		JFrame app = new JFrame("Game Core");
		app.getContentPane().add(ttPanel, BorderLayout.CENTER);
		app.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

		app.pack();
		app.setResizable(false);
		app.setVisible(true);
	} // end of main()

	public GamePanel() {
		instance = this;
		setBackground(Color.white); // white background
		setPreferredSize(new Dimension(PWIDTH, PHEIGHT));
		
		CriarPersonagens();
		
		gui = new Canvas();
		gui.setSize(WIDTH, HEIGHT);
		setLayout(new BorderLayout());
		add(gui, BorderLayout.CENTER);

		setFocusable(true); // create game components
		requestFocus(); // JPanel now receives key events

	} // end of GamePanel()

	public void CriarPersonagens() {
		heroi = new Heroi("Heroi", Color.blue, -10, PHEIGHT/2 - 25, 50, 50);
		inimigo = new Inimigo("Inimigo", Color.yellow, PWIDTH/2 - 25, PHEIGHT/2 - 25, 50, 50);
		inimigosCriados.add(inimigo);
	}
	
	public void addNotify() {
		super.addNotify(); // creates the peer
		startGame(); // start the thread
	}

	private void startGame()
	// initialise and start the thread
	{
		if (animator == null || !running) {
			animator = new Thread(this);
			animator.start();
		}
	} // end of startGame()

	public void stopGame()
	// called by the user to stop execution
	{
		running = false;
	}

	public void run()
	/* Repeatedly update, render, sleep */
	{
		running = true;
		long diffTime, previousTime;
		diffTime = 0;
		previousTime = System.currentTimeMillis();
		CurrentSecond = (long) (previousTime / 1000);

		gui.createBufferStrategy(2);
		strategy = gui.getBufferStrategy();

		while (running) {
			RodarJogo();
		}
		System.exit(0); // so enclosing JFrame/JApplet exits
	} // end of run()
	
	private void RodarJogo(){
		SetarGraphics2D();
		ChecarPersonagens(diffTime);
		RenderizarJogo();
		
		dbg.dispose();
		strategy.show();

		try {
			Thread.sleep(1); // sleep a bit
		} catch (InterruptedException ex) {
		}
		
		TratarVariacaoDoTempo();
	}

	private void ChecarPersonagens(long diffTime) {
		ChecarPlayer();
		ChecarInimigos();		
	}

	private void RenderizarJogo()
	// draw the current frame to an image buffer
	{
		// clear the background
		dbg.setColor(Color.black);
		dbg.fillRect(0, 0, PWIDTH, PHEIGHT); 	
		 
		// draw game elements
		dbg.setColor(Color.WHITE);
		dbg.drawString("FPS: " + FPS, 20, 20);

	} // end of gameRender()
	
	public static float calDist(float x1, float x2, float y1, float y2) {
		
		double dist = Math.sqrt(((x2-x1)*(x2-x1)) + ((y2-y1)*(y2 - y1)));
		
		return (float) dist;
	}

	public BufferedImage loadImage(String filename) {

		try {
			BufferedImage imgtmp = ImageIO.read(getClass().getResource(filename));
			BufferedImage imgfinal = new BufferedImage(imgtmp.getWidth(), imgtmp.getHeight(),
					BufferedImage.TYPE_INT_ARGB);
			imgfinal.getGraphics().drawImage(imgtmp, 0, 0, null);
			return imgfinal;
		} catch (IOException e) {
			e.printStackTrace();
			return null;
		}
	}

	private void SetarGraphics2D(){
		dbg = (Graphics2D) strategy.getDrawGraphics();
		dbg.setClip(0, 0, PWIDTH, PHEIGHT);
		dbg.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
	}

	private void ChecarPlayer(){
		heroi.Mover(diffTime);
		heroi.Renderizar(dbg);
	}

	private void ChecarInimigos(){

		for(int i = 0, i < inimigosCriados.Count, i++)
		{
			Inimigo _inimigo = inimigosCriados[i];
			_inimigo.ChecarEstadoAtual(heroi, diffTime);
			_inimigo.AplicarAcaoParaEstadoAtual(diffTime);
			_inimigo.Renderizar(dbg);
		}
	}

	private void TratarVariacaoDoTempo(){
		diffTime = System.currentTimeMillis() - previousTime;
			previousTime = System.currentTimeMillis();
			NewSecond = (long) (previousTime / 1000);
			if (NewSecond != CurrentSecond) {
				FPS = SFPS;
				CurrentSecond = NewSecond;
				SFPS = 1;
			} else {
				SFPS++;
			}
	}

} // end of GamePanel class
