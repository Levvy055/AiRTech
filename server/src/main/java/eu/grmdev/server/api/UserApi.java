/**
 * 
 */
package eu.grmdev.server.api;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.PUT;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import org.hibernate.HibernateException;
import org.json.JSONObject;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.github.fluent.hibernate.H;
import com.github.fluent.hibernate.request.HibernateRequest;

import eu.grmdev.server.rest.Token;
import eu.grmdev.server.rest.User;
import eu.grmdev.server.utils.Result;

/**
 * @author Levvy055
 */
@Path("user")
public class UserApi {

	@POST
	@Path("/{username}")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response register(@PathParam("username") String username, String payload) {
		if (username == null || username.isEmpty() || payload == null || payload.isEmpty()) {
			return Result.badRequest(true, null, username, payload);
		}
		try {
			ObjectMapper mapper = new ObjectMapper();
			mapper.setDateFormat(new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSSSSS"));
			User user = mapper.readValue(payload, User.class);
			if (user == null) {
				return Result.badRequest(true, "Payload wrong format or incomplete");
			}
			if (!username.toLowerCase().equals(user.getUsername().toLowerCase())) {
				return Result.badRequest(true, "Name in parameter is not the same as in body.");
			}
			if (user.getPassword() == null) {
				return Result.badRequest(true, "No password provided.");
			}
			if (user.getEmail() == null) {
				return Result.badRequest(true, "No e-mail provided.");
			}
			user.setUsername(user.getUsername().toLowerCase());
			user.setRegisterDate(new Date());
			user.setLastActive(new Date());
			User user2 = H.save(user);
			if (user.equals(user2)) {
				return Result.created(false, "User created successfully");
			} else {
				return Result.created(true,
						"User propably created because some problems occured during request execution.");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}

	@Path("/{username}")
	@GET
	@Produces(MediaType.APPLICATION_JSON)
	public Response get(@PathParam("username") String username, @QueryParam("authToken") String token) {
		try {
			HibernateRequest<User> request = H.<User>request(User.class);
			List<User> userList = request.fetchJoin("friends", "characters").eq("username", username).list();
			User user = null;
			if (userList == null || userList.isEmpty() || (user = userList.get(0)) == null) {
				return Result.notFound(true, false, "User with name " + username + " was not found");
			}
			String res;
			if (token != null && !token.isEmpty() && Token.verify(token, user.getId())) {
				res = user.toString();
				user.setLastActive(new Date());
			} else {
				res = "{\"id\": " + user.getId() + ", \"name\": \"" + user.getUsername() + "\"}";
			}
			System.out.println("name: " + username + "\ntoken: " + token);
			return Result.json(res);
		} catch (HibernateException e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}

	@PUT
	@Path("/pswd/{username}")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response changePassword(@PathParam("username") String username, String payload) {
		try {
			JSONObject jObj = new JSONObject(payload);
			String token = jObj.getString("authToken");
			if (!token.equals("token")) {
				return Result.noAuth(false, true, "Wrong token: " + token);
			}
			username = username.toLowerCase();
			if (!username.equals(jObj.getString("username").toLowerCase())) {
				return Result.noAuth(false, true, "No access for that user!");
			}
			String oldPswd = jObj.getString("oldPassword");
			String newPswd = jObj.getString("newPassword");
			User user = H.<User>request(User.class).eq("username", username).first();
			if (user == null) {
				return Result.notFound(false, true, "User '" + username + "' not found");
			}
			if (!user.getPassword().equals(oldPswd)) {
				return Result.noAuth(false, true, "Wrong credentials!");
			}
			if (newPswd == null || newPswd.length() <= 4) {
				return Result.badRequest(true, "Wrong Password! It should be longer than 3 letters.");
			}
			user.setPassword(newPswd);
			user.setLastActive(new Date());
			H.saveOrUpdate(user);
			return Result.success("Password updated");
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}

	@PUT
	@Path("/mail/{username}")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response changeEmail(@PathParam("username") String username, String payload) {
		try {
			JSONObject jObj = new JSONObject(payload);
			String token = jObj.getString("authToken");
			if (!token.equals("token")) {
				return Result.noAuth(false, true, "Wrong token: " + token);
			}
			username = username.toLowerCase();
			if (!username.equals(jObj.getString("username").toLowerCase())) {
				return Result.noAuth(false, true, "No access for that user!");
			}
			String mail = jObj.getString("mail");
			Pattern pattern = Pattern.compile(
					"^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$");
			Matcher matcher = pattern.matcher(mail);
			if (mail == null || mail.length() <= 4 || !matcher.matches()) {
				return Result.badRequest(true, "No mail provided or wrong mail syntax!");
			}
			User user = H.<User>request(User.class).eq("username", username).first();
			if (user == null) {
				return Result.notFound(false, true, "User '" + username + "' not found");
			}
			user.setLastActive(new Date());
			user.setEmail(mail);
			H.saveOrUpdate(user);
			return Result.success("Mail updated");
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}

	@DELETE
	@Path("/{username}")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response delete(@PathParam("username") String username, String payload) {
		try {
			JSONObject jObj = new JSONObject(payload);
			String token = jObj.getString("authToken");
			if (!token.equals("token")) {
				return Result.noAuth(false, true, "Wrong token: " + token);
			}
			username = username.toLowerCase();
			if (!username.equals(jObj.getString("username").toLowerCase())) {
				return Result.noAuth(false, true, "No access for that user!");
			}
			String pswd = jObj.getString("password");
			User user = H.<User>request(User.class).eq("username", username).first();
			if (!user.getPassword().equals(pswd)) {
				return Result.noAuth(false, true, "Wrong credentials!");
			}
			H.delete(user);
			return Result.success("User '" + username + "' deleted");
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}
}
