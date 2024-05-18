using System;
using UnityEngine;

namespace Cairo.CacheBoxing
{
    public abstract class BoxedValueType<T>
    {
        public abstract T Value { get; set; }
    }

    // Int
    public class BoxedInt : BoxedValueType<int>
    {
        private int _value = 0;
        public override int Value { get => _value; set => _value = value; }

        public static implicit operator BoxedInt(int value) => new BoxedInt { Value = value };
        public static explicit operator int(BoxedInt boxed) => boxed.Value;
    }

    // Float
    public class BoxedFloat : BoxedValueType<float>
    {
        private float _value = 0;
        public override float Value { get => _value; set => _value = value; }

        public static implicit operator BoxedFloat(float value) => new BoxedFloat { Value = value };
        public static explicit operator float(BoxedFloat boxed) => boxed.Value;
    }

    // Boolean
    public class BoxedBool : BoxedValueType<bool>
    {
        private bool _value = false;
        public override bool Value { get => _value; set => _value = value; }

        public static implicit operator BoxedBool(bool value) => new BoxedBool { Value = value };
        public static explicit operator bool(BoxedBool boxed) => boxed.Value;
    }

    // Byte
    public class BoxedByte : BoxedValueType<byte>
    {
        private byte _value = 0;
        public override byte Value { get => _value; set => _value = value; }

        public static implicit operator BoxedByte(byte value) => new BoxedByte { Value = value };
        public static explicit operator byte(BoxedByte boxed) => boxed.Value;
    }

    // Char
    public class BoxedChar : BoxedValueType<char>
    {
        private char _value = '\0';
        public override char Value { get => _value; set => _value = value; }

        public static implicit operator BoxedChar(char value) => new BoxedChar { Value = value };
        public static explicit operator char(BoxedChar boxed) => boxed.Value;
    }

    // Decimal
    public class BoxedDecimal : BoxedValueType<decimal>
    {
        private decimal _value = 0;
        public override decimal Value { get => _value; set => _value = value; }

        public static implicit operator BoxedDecimal(decimal value) => new BoxedDecimal { Value = value };
        public static explicit operator decimal(BoxedDecimal boxed) => boxed.Value;
    }

    // Double
    public class BoxedDouble : BoxedValueType<double>
    {
        private double _value = 0;
        public override double Value { get => _value; set => _value = value; }

        public static implicit operator BoxedDouble(double value) => new BoxedDouble { Value = value };
        public static explicit operator double(BoxedDouble boxed) => boxed.Value;
    }

    // Int16 (short)
    public class BoxedInt16 : BoxedValueType<short>
    {
        private short _value = 0;
        public override short Value { get => _value; set => _value = value; }

        public static implicit operator BoxedInt16(short value) => new BoxedInt16 { Value = value };
        public static explicit operator short(BoxedInt16 boxed) => boxed.Value;
    }

    // Int32 (int)
    public class BoxedInt32 : BoxedValueType<int>
    {
        private int _value = 0;
        public override int Value { get => _value; set => _value = value; }

        public static implicit operator BoxedInt32(int value) => new BoxedInt32 { Value = value };
        public static explicit operator int(BoxedInt32 boxed) => boxed.Value;
    }

    // Int64 (long)
    public class BoxedInt64 : BoxedValueType<long>
    {
        private long _value = 0;
        public override long Value { get => _value; set => _value = value; }

        public static implicit operator BoxedInt64(long value) => new BoxedInt64 { Value = value };
        public static explicit operator long(BoxedInt64 boxed) => boxed.Value;
    }

    // SByte
    public class BoxedSByte : BoxedValueType<sbyte>
    {
        private sbyte _value = 0;
        public override sbyte Value { get => _value; set => _value = value; }

        public static implicit operator BoxedSByte(sbyte value) => new BoxedSByte { Value = value };
        public static explicit operator sbyte(BoxedSByte boxed) => boxed.Value;
    }

    // UInt16
    public class BoxedUInt16 : BoxedValueType<ushort>
    {
        private ushort _value = 0;
        public override ushort Value { get => _value; set => _value = value; }

        public static implicit operator BoxedUInt16(ushort value) => new BoxedUInt16 { Value = value };
        public static explicit operator ushort(BoxedUInt16 boxed) => boxed.Value;
    }

    // UInt32
    public class BoxedUInt32 : BoxedValueType<uint>
    {
        private uint _value = 0;
        public override uint Value { get => _value; set => _value = value; }

        public static implicit operator BoxedUInt32(uint value) => new BoxedUInt32 { Value = value };
        public static explicit operator uint(BoxedUInt32 boxed) => boxed.Value;
    }

    // UInt64
    public class BoxedUInt64 : BoxedValueType<ulong>
    {
        private ulong _value = 0;
        public override ulong Value { get => _value; set => _value = value; }

        public static implicit operator BoxedUInt64(ulong value) => new BoxedUInt64 { Value = value };
        public static explicit operator ulong(BoxedUInt64 boxed) => boxed.Value;
    }

    // Vector2
    [Serializable]
    public class BoxedVector2 : BoxedValueType<Vector2>
    {
        private Vector2 _value = Vector2.zero;
        public override Vector2 Value { get => _value; set => _value = value; }

        public static implicit operator BoxedVector2(Vector2 value) => new BoxedVector2 { Value = value };
        public static explicit operator Vector2(BoxedVector2 boxed) => boxed.Value;
    }

    // Vector3
    public class BoxedVector3 : BoxedValueType<Vector3>
    {
        private Vector3 _value = Vector3.zero;
        public override Vector3 Value { get => _value; set => _value = value; }

        public static implicit operator BoxedVector3(Vector3 value) => new BoxedVector3 { Value = value };
        public static explicit operator Vector3(BoxedVector3 boxed) => boxed.Value;
    }

    // Vector4
    public class BoxedVector4 : BoxedValueType<Vector4>
    {
        private Vector4 _value = Vector4.zero;
        public override Vector4 Value { get => _value; set => _value = value; }

        public static implicit operator BoxedVector4(Vector4 value) => new BoxedVector4 { Value = value };
        public static explicit operator Vector4(BoxedVector4 boxed) => boxed.Value;
    }

    // Quaternion
    public class BoxedQuaternion : BoxedValueType<Quaternion>
    {
        private Quaternion _value = Quaternion.identity;
        public override Quaternion Value { get => _value; set => _value = value; }

        public static implicit operator BoxedQuaternion(Quaternion value) => new BoxedQuaternion { Value = value };
        public static explicit operator Quaternion(BoxedQuaternion boxed) => boxed.Value;
    }

    // Color
    public class BoxedColor : BoxedValueType<Color>
    {
        private Color _value = Color.black;
        public override Color Value { get => _value; set => _value = value; }

        public static implicit operator BoxedColor(Color value) => new BoxedColor { Value = value };
        public static explicit operator Color(BoxedColor boxed) => boxed.Value;
    }

    // Rect
    public class BoxedRect : BoxedValueType<Rect>
    {
        private Rect _value = Rect.zero;
        public override Rect Value { get => _value; set => _value = value; }

        public static implicit operator BoxedRect(Rect value) => new BoxedRect { Value = value };
        public static explicit operator Rect(BoxedRect boxed) => boxed.Value;
    }

    // Bounds
    public class BoxedBounds : BoxedValueType<Bounds>
    {
        private Bounds _value = new Bounds();
        public override Bounds Value { get => _value; set => _value = value; }

        public static implicit operator BoxedBounds(Bounds value) => new BoxedBounds { Value = value };
        public static explicit operator Bounds(BoxedBounds boxed) => boxed.Value;
    }

    // RaycastHit
    public class BoxedRaycastHit : BoxedValueType<RaycastHit>
    {
        private RaycastHit _value = new RaycastHit();
        public override RaycastHit Value { get => _value; set => _value = value; }

        public static implicit operator BoxedRaycastHit(RaycastHit value) => new BoxedRaycastHit { Value = value };
        public static explicit operator RaycastHit(BoxedRaycastHit boxed) => boxed.Value;
    }
}