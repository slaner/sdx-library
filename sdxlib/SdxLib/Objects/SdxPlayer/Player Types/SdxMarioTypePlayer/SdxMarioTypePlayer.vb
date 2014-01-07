Public Class SdxMarioTypePlayer
    Inherits SdxPlayer

    Private g_InGround As Boolean = True
    Private g_Jumping As Boolean = False
    Private m_CanJump As Boolean = True
    Private m_JumpStep As Int32
    Private m_JumpPressed As Boolean

    Public Const JUMP_POWER = 5
    Public Const JUMP_STEP_INCREASEMENT = 2
    Public Const EXTRA_JUMP_CONST = 2

End Class