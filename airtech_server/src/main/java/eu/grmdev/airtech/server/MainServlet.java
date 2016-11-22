/**
 * 
 */
package eu.grmdev.airtech.server;

import java.io.IOException;

import javax.servlet.ServletOutputStream;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * @author Levvy055
 */
public class MainServlet extends HttpServlet {
	
	private static final long serialVersionUID = -4431482150325195432L;
	
	@Override
	protected void doGet(HttpServletRequest req, HttpServletResponse resp) {
		try {
			ServletOutputStream os = resp.getOutputStream();
			os.write("Hello from AiRTech Server ;)  V: 0.0.0.0001".getBytes());
		}
		catch (IOException e) {
			e.printStackTrace();
		}
	}
}
