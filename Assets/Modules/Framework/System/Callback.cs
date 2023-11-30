public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
public delegate void Callback<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);
public delegate void CallbackRef<T>(ref T arg1);
public delegate void CallbackRef<T, U>(ref T arg1, ref U arg2);
public delegate void CallbackRef<T, U, V>(ref T arg1, ref U arg2, ref V arg3);
public delegate void CallbackRef<T, U, V, W>(ref T arg1, ref U arg2, ref V arg3, ref W arg4);