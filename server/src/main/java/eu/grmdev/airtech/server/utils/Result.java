/**
 * 
 */
package eu.grmdev.airtech.server.utils;

import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.ResponseBuilder;
import javax.ws.rs.core.Response.Status;

import org.json.JSONObject;

/**
 * @author Levvy055
 */
public class Result {
	
	private boolean success;
	private boolean error;
	private String msg;
	private boolean json;
	private JSONObject jsonObject;
	
	/**
	 * @param success
	 * @param msg
	 */
	private Result(boolean success, boolean error, String msg) {
		this.success = success;
		this.msg = msg;
		this.error = error;
		this.json = false;
	}
	
	private Result(boolean success, boolean error, JSONObject jObj) {
		this(success, error, "");
		this.json = true;
		this.jsonObject = jObj;
	}
	
	public static Response json(String json) {
		StackTraceElement[] stackTrace = new Exception().getStackTrace();
		StackTraceElement stackTraceElement = stackTrace[1];
		String callerClassName = stackTraceElement.getClassName();
		String methodName = stackTraceElement.getMethodName();
		int lineNumber = stackTraceElement.getLineNumber();
		CLogger.info("{\"class\": \"" + callerClassName + "\",\"method\": \"" + methodName + "\",\"line\": " + lineNumber + ",\"obj\": " + json + "}");
		return Response.ok(json).build();
	}
	
	public static Response success(String info) {
		if (info == null || info.isEmpty()) { return Response.ok().build(); }
		return Response.ok(new Result(true, false, info).asJson()).build();
	}
	
	public static Response noAuth(boolean success, boolean error, String obj) {
		return getStatusResponse(success, error, obj, Status.UNAUTHORIZED).build();
	}
	
	public static Response notFound(boolean success, boolean error, String obj) {
		return getStatusResponse(success, error, obj, Status.NOT_FOUND).build();
	}
	
	/**
	 * @param error
	 *           true if there was warning during execution
	 * @param msg
	 *           message to send
	 * @return {@link Response}
	 */
	public static Response created(boolean error, String msg) {
		return Response.status(Status.CREATED).entity(new Result(true, error, msg).asJson()).build();
	}
	
	/**
	 * @param error
	 *           true if there was warning during execution
	 * @param jObj
	 *           iinner json object
	 * @return {@link Response}
	 */
	public static Response created(boolean error, JSONObject jObj) {
		return Response.status(Status.CREATED).entity(new Result(true, error, jObj).asJson()).build();
	}
	
	/**
	 * @param error
	 *           true if there was error during execution
	 * @param msg
	 *           message to send
	 * @param params
	 *           additional parameters vars
	 * @return {@link Response}
	 */
	public static Response badRequest(boolean error, String msg, String... params) {
		msg = "{\"Message\": \"" + msg;
		if (params != null && params.length > 0) {
			msg += "\",\"Parameters\": [";
			for (int i = 0; i < params.length; i++) {
				String param = params[i];
				msg += "\"Param " + i + "\": \"";
				if (param == null) {
					msg += "NULL";
				}
				else {
					msg += param;
				}
				msg += "\",";
			}
			msg = msg.substring(0, msg.length() - 1) + "]}";
		}
		ResponseBuilder respBuilder = getStatusResponse(false, error, msg, Status.BAD_REQUEST);
		return respBuilder.build();
	}
	
	private static ResponseBuilder getStatusResponse(boolean success, boolean error, String obj, Status status) {
		ResponseBuilder respB = Response.status(status);
		respB = respB.entity(new Result(success, error, obj).toString());
		CLogger.info("{\"success\": " + success + ",\"error\": " + error + ", \"obj\": " + (obj == null ? "null" : "\"" + obj + "\"") + ", \"status\": \"" + status.toString() + "\"");
		return respB;
	}
	
	public static Response exception(Exception e) {
		return Result.exception(e, "Exception in request execution");
	}
	
	public static Response exception(Exception e, String info) {
		return Response.status(500).entity(new Result(false, true, info + " // " + e.getMessage()).asJson()).build();
	}
	
	/**
	 * @return Result object in JSON format
	 */
	public String asJson() {
		String r = toString();
		StackTraceElement[] stackTrace = new Exception().getStackTrace();
		StackTraceElement stackTraceElement = stackTrace[1];
		if (stackTraceElement.getClassName().equals(this.getClass().getName())) {
			stackTraceElement = stackTrace[2];
		}
		String callerClassName = stackTraceElement.getClassName();
		String methodName = stackTraceElement.getMethodName();
		int lineNumber = stackTraceElement.getLineNumber();
		CLogger.info("{\"class\": \"" + callerClassName + "\",\"method\": \"" + methodName + "\",\"line\": " + lineNumber + ",\"obj\": " + r + "}");
		return r;
	}
	
	@Override
	public String toString() {
		StringBuilder builder = new StringBuilder();
		builder.append("{\"success\": ");
		builder.append(success);
		builder.append(",\"error\": ");
		builder.append(error);
		if (msg != null && !msg.isEmpty()) {
			builder.append(",\"");
			if (error) {
				builder.append("error_msg\":\"");
			}
			else {
				builder.append("message\":\"");
			}
			builder.append(msg.replaceAll("\"", "'"));
			builder.append("\"");
		}
		if (json) {
			builder.append(", \"response\":");
			builder.append(jsonObject.toString());
			builder.append("}");
		}
		else {
			builder.append(builder.charAt(builder.length() - 1) != '\"' ? "\"} " : "} ");
		}
		return builder.toString();
	}
}
