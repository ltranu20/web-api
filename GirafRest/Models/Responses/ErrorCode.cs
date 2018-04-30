﻿namespace GirafRest.Models.Responses
{
    public enum ErrorCode
    {
        Error,
        FormatError,
        NoError,
        NotAuthorized,
        NotFound,
        ApplicationNotFound,
        ChoiceContainsInvalidPictogramId,
        CitizenAlreadyHasGuardian,
        CitizenNotFound,
        DepartmentAlreadyOwnsResource,
        DepartmentNotFound,
        EmailServiceUnavailable,
        ImageAlreadyExistOnPictogram,
        ImageNotContainedInRequest,
        InvalidCredentials,
        InvalidModelState,
        InvalidProperties,
        MissingProperties,
        NoWeekScheduleFound,
        PasswordNotUpdated,
        PictogramHasNoImage,
        PictogramNotFound,
        QueryFailed,
        ResourceMustBePrivate,
        ResourceNotFound,
        ResourceNotOwnedByDepartment,
        ResourceIDUnreadable,
        RoleMustBeCitizien,
        RoleNotFound,
        ThumbnailDoesNotExist,
        UserAlreadyExists,
        UserNameAlreadyTakenWithinDepartment,
        UserAlreadyHasAccess,
        UserAlreadyHasIconUsePut,
        UserAlreadyOwnsResource,
        UserAndCitizenMustBeInSameDepartment,
        UserCannotBeGuardianOfYourself,
        UserDoesNotOwnResource,
        UserHasNoIcon,
        UserHasNoIconUsePost,
        UserMustBeGuardian,
        UserNotFound,
        WeekScheduleNotFound,
        Forbidden,
        PasswordMissMatch,
        TwoDaysCannotHaveSameDayProperty,
        UserHasNoCitizens,
        UserHasNoGuardians,
        DepartmentHasNoCitizens,
        UnknownError,
        CouldNotCreateDepartmentUser,
        UserNotFoundInDepartment,
        NoWeekTemplateFound,
        UserAlreadyHasDepartment,
        MissingSettings,
        DuplicateWeekScheduleName
    }
}
