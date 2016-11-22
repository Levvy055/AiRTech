/**
 * 
 */
package eu.grmdev.airtech.server.models;

import java.util.Date;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.Table;

import lombok.Getter;
import lombok.Setter;

/**
 * @author Levvy055
 */
@Entity
@Table(name = "users")
public class User {
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Getter
	@Setter
	private int id;
	@Column(name = "f_name", unique = true, nullable = false)
	@Getter
	@Setter
	private String username;
	@Column(name = "f_pasword", nullable = false)
	@Getter
	@Setter
	private String password;
	@Column(name = "f_email", nullable = false, unique = true)
	@Getter
	@Setter
	private String email;
	@Column(name = "register_date", nullable = false)
	@Getter
	@Setter
	private Date registerDate;
	@Column(name = "time_last_active")
	@Getter
	@Setter
	private Date lastActive;
	@Column(name = "f_user_level", nullable = false)
	@Getter
	@Setter
	private int level;
	@OneToMany(fetch = FetchType.LAZY, cascade = CascadeType.ALL)
	@Getter
	@Setter
	private List<Token> tokens;
	public static final String USERNAME_PATTERN = "^[a-z0-9A-Z_-]{3,15}$";
	
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((email == null) ? 0 : email.hashCode());
		result = prime * result + id;
		result = prime * result + ((username == null) ? 0 : username.hashCode());
		result = prime * result + ((password == null) ? 0 : password.hashCode());
		result = prime * result + ((registerDate == null) ? 0 : registerDate.hashCode());
		return result;
	}
	
	@Override
	public boolean equals(Object obj) {
		if (this == obj) return true;
		if (obj == null) return false;
		if (getClass() != obj.getClass()) return false;
		User other = (User) obj;
		if (email == null) {
			if (other.email != null) return false;
		}
		else if (!email.equals(other.email)) return false;
		if (id != other.id) return false;
		if (username == null) {
			if (other.username != null) return false;
		}
		else if (!username.equals(other.username)) return false;
		if (password == null) {
			if (other.password != null) return false;
		}
		else if (!password.equals(other.password)) return false;
		if (registerDate == null) {
			if (other.registerDate != null) return false;
		}
		else if (!registerDate.equals(other.registerDate)) return false;
		return true;
	}
	
	@Override
	public String toString() {
		StringBuilder builder = new StringBuilder();
		builder.append("{\"id\":\"");
		builder.append(id);
		builder.append("\",\"");
		builder.append("name\":\"");
		builder.append(username);
		if (registerDate != null) {
			builder.append("\",\"");
			builder.append("registerDate\":\"");
			builder.append(registerDate);
		}
		if (lastActive != null) {
			builder.append("\",\"");
			builder.append("lastActive\":\"");
			builder.append(lastActive);
		}
		if (email != null) {
			builder.append("\",\"");
			builder.append("email\":\"");
			builder.append(email);
		}
		builder.append("\",\"");
		builder.append("level\":\"");
		builder.append(level);
		builder.append("\"} ");
		return builder.toString();
	}
	
}
