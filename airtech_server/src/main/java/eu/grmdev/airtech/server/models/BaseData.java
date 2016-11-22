package eu.grmdev.airtech.server.models;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;

import lombok.Getter;
import lombok.Setter;

@Entity
@Table(name = "base_data")
public class BaseData {
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	@Getter
	@Setter
	private int id;
	@Getter
	@Setter
	@Column(name = "ver_and", nullable = false)
	private String Android_Version;
	@Getter
	@Setter
	@Column(name = "ver_ios", nullable = false)
	private String iOS_Version;
	@Getter
	@Setter
	@Column(name = "ver_uwp", nullable = false)
	private String UWP_Version;
	@Getter
	@Setter
	@Column(name = "ver_api", nullable = false)
	private String Api_Version;
	
	@Override
	public String toString() {
		return getClass().getName() + " {\n\t{Android_Version: " + Android_Version + "},\n\tiOS_Version: " + iOS_Version + "},\n\tUWP_Version: " + UWP_Version + "},\n\tApi_Version: " + Api_Version
			+ "\n}";
	}
}
