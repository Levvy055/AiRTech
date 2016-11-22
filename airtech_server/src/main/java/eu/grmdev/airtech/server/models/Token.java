/**
* 
*/
package eu.grmdev.airtech.server.models;

import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Random;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import com.github.fluent.hibernate.H;

import lombok.Getter;
import lombok.Setter;

/**
 * @author Levvy055
 */
@Entity
@Table(name = "tokens")
public class Token {

	@Id
	@GeneratedValue
	@Getter
	@Setter
	private int id;
	@Column(name = "f_token", nullable = false, unique = true)
	@Getter
	@Setter
	private char[] token;
	@Column(name = "f_expiration_time", nullable = false)
	@Getter
	@Setter
	private Date expirationTime;
	@JoinColumn(name = "user_id", nullable = false)
	@ManyToOne(fetch = FetchType.LAZY)
	@Getter
	@Setter
	private User user;

	public static final Random RANDOM = new Random();
	public static final String CHARS = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ234567890!@_-$";

	/**
	 * @param token
	 *            Token object to verify
	 * @param username
	 *            username
	 * @return true if token is valid
	 */
	public static boolean verify(String token, String username) {
		if (token != null && !token.isEmpty()) {
			List<Token> tokenList = H.<Token>request(Token.class).fetchJoin("user").eq("token", token.toCharArray())
					.list();
			if (tokenList != null && tokenList.size() > 0) {
				Token tokenObj = tokenList.get(0);
				if (tokenObj != null && tokenObj.getUser().getUsername().equals(username)
						&& tokenObj.getExpirationTime().after(new Date())) {
					return true;
				}
			}
		}
		return false;
	}

	/**
	 * @param token
	 *            Token object to verify
	 * @param userId
	 *            user_id
	 * @return true if token is valid
	 */
	public static boolean verify(String token, int userId) {
		if (token != null && !token.isEmpty()) {
			List<Token> tokenList = H.<Token>request(Token.class).fetchJoin("user").eq("token", token.toCharArray())
					.list();
			if (tokenList != null && tokenList.size() > 0) {
				Token tokenObj = tokenList.get(0);
				if (tokenObj != null && tokenObj.getUser().getId() == userId
						&& tokenObj.getExpirationTime().after(new Date())) {
					return true;
				}
			}
		}
		return false;
	}

	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((expirationTime == null) ? 0 : expirationTime.hashCode());
		result = prime * result + id;
		result = prime * result + Arrays.hashCode(token);
		result = prime * result + ((user == null) ? 0 : user.hashCode());
		return result;
	}

	@Override
	public boolean equals(Object obj) {
		if (this == obj)
			return true;
		if (obj == null)
			return false;
		if (getClass() != obj.getClass())
			return false;
		Token other = (Token) obj;
		if (expirationTime == null) {
			if (other.expirationTime != null)
				return false;
		} else if (!expirationTime.equals(other.expirationTime))
			return false;
		if (id != other.id)
			return false;
		if (!Arrays.equals(token, other.token))
			return false;
		if (user == null) {
			if (other.user != null)
				return false;
		} else if (!user.equals(other.user))
			return false;
		return true;
	}
}