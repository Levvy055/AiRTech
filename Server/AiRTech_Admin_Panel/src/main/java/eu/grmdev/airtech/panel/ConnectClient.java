package eu.grmdev.airtech.panel;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;
import org.springframework.web.client.RestOperations;

@Component
public class ConnectClient {
	
	@Autowired
	private RestOperations restOperations;
	private final String url;
	
	@Autowired
	public ConnectClient(@Value("$server.rest.url")
	final String url) {
		this.url = url;
	}
}
