﻿using System;
using ObjCRuntime;


namespace HockeyApp.iOS
{    
    [Native]
    public enum BITLogLevel : ulong /* nuint */ {
        None = 0,
        Error = 1,
        Warning = 2,
        Debug = 3,
        Verbose = 4
    }

    [Native]
    public enum BITEnvironment : long {
        AppStore = 0,
        TestFlight = 1,
        Other = 99
    }

    [Native]
    public enum BITAuthenticatorIdentificationType : ulong /* nuint */ {
        Anonymous,
        HockeyAppEmail,
        HockeyAppUser,
        Device,
        WebAuth
    }

    [Native]
    public enum BITAuthenticatorAppRestrictionEnforcementFrequency : ulong /* nuint */ {
        FirstLaunch,
        AppActive
    }

    [Native]
    public enum BITCrashManagerStatus : ulong /* nuint */ {
        Disabled = 0,
        AlwaysAsk = 1,
        AutoSend = 2
    }

//    public struct BITCrashManagerCallbacks
//    {
//        unsafe void* context;
//
//        unsafe BITCrashManagerPostCrashSignalCallback* handleSignal;
//    }

    [Native]
    public enum BITCrashManagerUserInput : ulong /* nuint */ {
        DontSend = 0,
        Send = 1,
        AlwaysSend = 2
    }

    #if !CRASHONLY
    [Native]
    public enum BITUpdateSetting : ulong /* nuint */ {
        CheckStartup = 0,
        CheckDaily = 1,
        CheckManually = 2
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITStoreUpdateSetting : long /* nint */ {
        CheckDaily = 0,
        CheckWeekly = 1,
        CheckManually = 2
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITFeedbackComposeResult : ulong /* nuint */ {
        Cancelled,
        Submitted
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITFeedbackUserDataElement : long /* nint */ {
        DontShow = 0,
        Optional = 1,
        Required = 2
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITFeedbackObservationMode : long /* nint */ {
        None = 0,
        OnScreenshot = 1,
        ThreeFingerTap = 2
    }
    #endif

    [Native]
    public enum BITCrashErrorReason : long /* nint */ {
        ErrorUnknown,
        APIAppVersionRejected,
        APIReceivedEmptyResponse,
        APIErrorWithStatusCode
    }

    #if !CRASHONLY
    [Native]
    public enum BITUpdateErrorReason : long /* nint */ {
        ErrorUnknown,
        APIServerReturnedInvalidStatus,
        APIServerReturnedInvalidData,
        APIServerReturnedEmptyResponse,
        APIClientAuthorizationMissingSecret,
        APIClientCannotCreateConnection
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITFeedbackErrorReason : long /* nint */ {
        ErrorUnknown,
        APIServerReturnedInvalidStatus,
        APIServerReturnedInvalidData,
        APIServerReturnedEmptyResponse,
        APIClientAuthorizationMissingSecret,
        APIClientCannotCreateConnection
    }
    #endif

    #if !CRASHONLY
    [Native]
    public enum BITAuthenticatorReason : long /* nint */ {
        ErrorUnknown,
        NetworkError,
        APIServerReturnedInvalidResponse,
        NotAuthorized,
        UnknownApplicationID,
        AuthorizationSecretMissing,
        NotIdentified
    }
    #endif

    [Native]
    public enum BITHockeyErrorReason : long /* nint */ {
        ErrorUnknown
    }

}
