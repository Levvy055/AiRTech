/**
* 
*/
package eu.grmdev.airtech.server.api;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.QueryParam;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

import org.apache.commons.lang3.StringUtils;
import org.json.JSONObject;

import com.github.fluent.hibernate.H;

import eu.grmdev.airtech.server.rest.Token;
import eu.grmdev.airtech.server.rest.User;
import eu.grmdev.airtech.server.utils.Result;

/**
 * @author Levvy055
 */
@Path("token")
public class TokenApi {

	@POST
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response generate(String body, @Context HttpServletRequest request) {
		if (StringUtils.isEmpty(body)) {
			return Result.badRequest(true, "Received empty request!");
		}
		try {
			JSONObject obj = new JSONObject(body);
			String username = obj.getString("username");
			String pswd = obj.getString("password");
			if (StringUtils.isEmpty(username) || StringUtils.isEmpty(pswd)) {
				return Result.badRequest(true, "Wrong credentials!");
			}
			User user = H.<User>request(User.class).fetchJoin("tokens").eq("username", username).eq("password", pswd)
					.first();
			if (user == null) {
				return Result.badRequest(true, "Wrong credentials!");
			}
			user.setLastActive(new Date());
			H.saveOrUpdate(user);
			char[] token = null;
			boolean exists = true;
			int attempts = 0;
			List<Token> tokens = user.getTokens();
			do {
				attempts++;
				token = genToken(username, user.getId());
				Iterator<Token> it = tokens.iterator();
				Token tokenTemp = null;
				while (it.hasNext()) {
					Token tT = it.next();
					if (tT.getToken().equals(token)) {
						tokenTemp = tT;
					}
				}
				exists = tokenTemp != null;
				if (attempts > 6) {
					return Result.exception(new Exception("There was problem with token generator."));
				}
			} while (exists);
			Token tokenObj = new Token();
			tokenObj.setToken(token);
			Calendar cal = Calendar.getInstance();
			cal.setTime(new Date());
			cal.add(Calendar.HOUR_OF_DAY, 3);
			tokenObj.setExpirationTime(cal.getTime());
			tokenObj.setUser(user);
			Token tokenRespond = H.save(tokenObj);
			startCleanupThread(user, request.getRemoteAddr());
			if (tokenObj.equals(tokenRespond)) {
				Map<String, String> map = new HashMap<>();
				map.put("msg", "Token generated");
				map.put("token", new String(token));
				return Result.created(false, new JSONObject(map));
			} else {
				return Result.created(true, "Token generated but smth went wrong.");
			}
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}

	/**
	 * @return
	 */
	private char[] genToken(String name, int id) {
		StringBuilder token = new StringBuilder();
		for (int i = 0; i < 120; i++) {
			token.append(Token.CHARS.charAt(Token.RANDOM.nextInt(Token.CHARS.length())));
		}
		return token.toString().toCharArray();
	}

	/**
	 * @param user
	 * @param address
	 */
	private void startCleanupThread(User user, String address) {
		Thread thread = new Thread(() -> {
			System.out.println("Started token cleanup thread for " + address);
			List<Object> list = H.request(TokenApi.class).eq("user", user).list();
			for (Object object : list) {
				Token token = (Token) object;
				if (token != null) {
					Date expTime = token.getExpirationTime();
					if (expTime.before(new Date())) {
						H.delete(token);
					}
				}
			}
		});
		thread.setName("token cleanup thread: " + address);
		thread.start();
	}

	@GET
	@Path("/{username}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response getExpirationDate(@PathParam("username") String username, @QueryParam("authToken") String token) {
		if (StringUtils.isEmpty(username) || StringUtils.isEmpty(token)) {
			return Result.badRequest(true, "No username specified and/or token", username, token);
		}
		System.out.println(token);
		try {
			Token tokenObj = H.<Token>request(Token.class).eq("token", token.toCharArray()).first();
			if (tokenObj == null) {
				return Result.notFound(false, true, "token not exists!");
			}
			return Result.success(tokenObj.getExpirationTime().toString());
		} catch (Exception e) {
			e.printStackTrace();
			return Result.exception(e);
		}
	}
}