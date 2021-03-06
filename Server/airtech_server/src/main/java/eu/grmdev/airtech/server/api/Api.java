package eu.grmdev.airtech.server.api;

import java.util.List;

import javax.ws.rs.GET;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;

import com.github.fluent.hibernate.H;

import eu.grmdev.airtech.server.models.BaseData;

@Path("/")
public class Api {
	
	@Path("status")
	@GET
	@Produces(MediaType.TEXT_PLAIN)
	public Response status() {
		return Response.ok("Working").build();
	}
	
	@Path("version")
	@GET
	@Produces(MediaType.TEXT_PLAIN)
	public Response version() {
		List<Object> list = H.request(BaseData.class).list();
		BaseData data = (BaseData) list.get(list.size() - 1);
		if (data == null) { return Response.status(Status.INTERNAL_SERVER_ERROR).build(); }
		return Response.ok(data.toString()).build();
	}
}
