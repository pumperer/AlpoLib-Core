//#define USE_NSLOG
using UnityEngine;
using System.Diagnostics;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

public static class Debug
{
	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		UnityEngine.Debug.DrawLine(start, end, color);
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void Log(object message)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Normal, message);
#else
		UnityEngine.Debug.Log(message);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void Log(object message, Object context)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Normal, message);
#else
		UnityEngine.Debug.Log(message, context);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogFormat(string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Normal, format, args);
#else
		UnityEngine.Debug.LogFormat(format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogFormat(Object context, string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Normal, format, args);
#else
		UnityEngine.Debug.LogFormat(context, format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogWarning(object message)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Warning, message);
#else
		UnityEngine.Debug.LogWarning(message);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogWarning(object message, Object context)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Warning, message);
#else
		UnityEngine.Debug.LogWarning(message, context);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogWarningFormat(string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Warning, format, args);
#else
		UnityEngine.Debug.LogWarningFormat(format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogWarningFormat(Object context, string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Warning, format, args);
#else
		UnityEngine.Debug.LogWarningFormat(context, format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogError(object message)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Error, message);
#else
		UnityEngine.Debug.LogError(message);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogError(object message, Object context)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Error, message);
#else
		UnityEngine.Debug.LogError(message, context);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogErrorFormat(string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Error, format, args);
#else
		UnityEngine.Debug.LogErrorFormat(format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogErrorFormat(Object context, string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Error, format, args);
#else
		UnityEngine.Debug.LogErrorFormat(context, format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogException(System.Exception exception)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Exception, exception.Message);
#else
		UnityEngine.Debug.LogException(exception);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogException(System.Exception exception, Object context)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Exception, exception.Message);
#else
		UnityEngine.Debug.LogException(exception, context);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogAssertion(object message)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Assertion, message);
#else
		UnityEngine.Debug.LogAssertion(message);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogAssertion(object message, Object context)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Assertion, message);
#else
		UnityEngine.Debug.LogAssertion(message, context);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogAssertionFormat(string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Assertion, format, args);
#else
		UnityEngine.Debug.LogAssertionFormat(format, args);
#endif
	}

	[Conditional("ENABLE_LOG"), Conditional("UNITY_EDITOR")]
	public static void LogAssertionFormat(Object context, string format, params object[] args)
	{
#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR
		DebugLogForIOS.NSLog(DebugLogForIOS.LogLevel.Assertion, format, args);
#else
		UnityEngine.Debug.LogAssertionFormat(context, format, args);
#endif
	}

	public static bool isDebugBuild { get { return UnityEngine.Debug.isDebugBuild; } }
}

#if UNITY_IOS && USE_NSLOG && !UNITY_EDITOR 
public class DebugLogForIOS
{
	public enum LogLevel
	{
		Normal,
		Warning,
		Error,
		Exception,
		Assertion,
	}

#if !UNITY_EDITOR
	[DllImport("__Internal")]
	private static extern void _NSLog(string message);
#endif
	
	public static void NSLog(LogLevel logLevel, object message)
	{
#if !UNITY_EDITOR
		string newMsg = string.Format("{0} - {1}", logLevel.ToString(), message);
		_NSLog(newMsg);
#endif
	}

	public static void NSLog(LogLevel logLevel, string format, params object[] args)
	{
#if !UNITY_EDITOR
        string newMsg = string.Empty;
        if (args != null && args.Length > 0)
            newMsg = string.Format(format, args);
		NSLog(logLevel, (object)newMsg);
#endif
	}
}
#endif