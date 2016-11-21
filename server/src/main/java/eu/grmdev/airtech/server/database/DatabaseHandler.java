/**
 *
 */
package eu.grmdev.airtech.server.database;

import com.github.fluent.hibernate.cfg.Fluent;

import eu.grmdev.airtech.server.rest.Token;
import eu.grmdev.airtech.server.rest.User;

/**
 * @author Levvy055
 */
public final class DatabaseHandler {
	
	private static volatile boolean configured;
	
	private DatabaseHandler() {}
	
	public static void initConnection() {
		if (configured) { return; }
		createSessionFactory();
	}
	
	private static synchronized void createSessionFactory() {
		if (configured) { return; }
		@SuppressWarnings("rawtypes")
		Class[] classes = new Class[]{User.class, Token.class};
		Fluent.factory().annotatedClasses(classes).build();
		configured = true;
	}
	
	public static void closeConnection() {
		Fluent.factory().close();
	}
	
}
