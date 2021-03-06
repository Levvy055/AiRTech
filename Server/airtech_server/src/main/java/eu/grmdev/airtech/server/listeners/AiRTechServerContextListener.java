/**
 * 
 */
package eu.grmdev.airtech.server.listeners;

import static eu.grmdev.airtech.server.utils.CLogger.closeLoggers;
import static eu.grmdev.airtech.server.utils.CLogger.info;
import static eu.grmdev.airtech.server.utils.CLogger.initLogger;
import static eu.grmdev.airtech.server.utils.CLogger.warn;

import javax.servlet.ServletContextEvent;
import javax.servlet.ServletContextListener;

import com.mysql.cj.jdbc.AbandonedConnectionCleanupThread;

import eu.grmdev.airtech.server.database.DatabaseHandler;

/**
 * @author Levvy055
 */
public class AiRTechServerContextListener implements ServletContextListener {
	
	/*
	 * (non-Javadoc)
	 * @see
	 * javax.servlet.ServletContextListener#contextInitialized(javax.servlet.
	 * ServletContextEvent)
	 */
	@Override
	public void contextInitialized(ServletContextEvent sce) {
		String logPath = sce.getServletContext().getRealPath("/");
		try {
			initLogger(logPath);
		}
		catch (Exception e) {
			info("__________________" + logPath);
		}
		info("AiR Tech Server App initializing ...");
		DatabaseHandler.initConnection();
		info("Connection initialized");
	}
	
	/*
	 * (non-Javadoc)
	 * @see javax.servlet.ServletContextListener#contextDestroyed(javax.servlet.
	 * ServletContextEvent)
	 */
	@Override
	public void contextDestroyed(ServletContextEvent sce) {
		info("AiR Tech Server App closing ...");
		DatabaseHandler.closeConnection();
		try {
			AbandonedConnectionCleanupThread.shutdown();
		}
		catch (InterruptedException e) {
			warn("SEVERE problem cleaning up: " + e.getMessage());
			e.printStackTrace();
		}
		closeLoggers();
	}
}
