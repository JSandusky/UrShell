#pragma once

#include <Urho3D/Urho3D.h>
#include <Urho3D/Math/Color.h>
#include <Urho3D/Math/Matrix3.h>
#include <Urho3D/Math/Matrix3x4.h>
#include <Urho3D/Math/Matrix4.h>
#include <Urho3D/Math/Rect.h>
#include <Urho3D/Math/Vector2.h>
#include <Urho3D/Math/Vector3.h>
#include <Urho3D/Math/Vector4.h>
#include <Urho3D/Math/Quaternion.h>

namespace UrhoBackend
{

public ref class MathVector
{
public:
    MathVector(int len) { Length = len; }
    property int Length;

    virtual MathVector^ Clone() {
        return nullptr;
    }

    float operator[](int index)
    {
        return Get(index);
    }

    virtual float Get(int index) { return 0.0f; }
    virtual void Set(int index, float value) {}
};

//[TypeConverter(Vector2Converter::typeid)]
[System::ComponentModel::EditorAttribute("EditorWinForms.Controls.PropertyGrid.VectorEditor, EditorWinForms", System::Drawing::Design::UITypeEditor::typeid)]
public ref class Vector2 : public MathVector
{
public:
    property float x;
    property float y;

    Vector2() : MathVector(2) { x = 0; y = 0; }
    Vector2(Vector2^ src) : MathVector(2) { x = src->x; y = src->y; }
    Vector2(float xx, float yy) : MathVector(2) { x = xx; y = yy; }
    Vector2(Urho3D::Vector2 vec) : MathVector(2) { x = vec.x_; y = vec.y_; }
    Urho3D::Vector2 ToVector2() { return Urho3D::Vector2(x, y); }

    virtual MathVector^ Clone() override {
        return gcnew Vector2(this);
    }

    virtual float Get(int index) override { 
        if (index == 0)
            return x;
        return y;
    }
    virtual void Set(int index, float value) override {
        if (index == 0)
            x = value;
        y = value;
    }

    Vector2^ operator+=(Vector2^ rhs) { x += rhs->x; y += rhs->y; return this; }
    Vector2^ operator-=(Vector2^ rhs) { x -= rhs->x; y -= rhs->y; return this; }
    Vector2^ operator*=(Vector2^ rhs) { x *= rhs->x; y *= rhs->y; return this; }
    Vector2^ operator*=(float rhs) { x *= rhs; y *= rhs; return this; }
    Vector2^ operator/=(Vector2^ rhs) { x /= rhs->x; y /= rhs->y; return this; }

    Vector2^ operator+(Vector2^ rhs) { return gcnew Vector2(x + rhs->x, y + rhs->y); }
    Vector2^ operator-(Vector2^ rhs) { return gcnew Vector2(x - rhs->x, y - rhs->y); }
    Vector2^ operator*(Vector2^ rhs) { return gcnew Vector2(x * rhs->x, y * rhs->y); }
    Vector2^ operator/(Vector2^ rhs) { return gcnew Vector2(x / rhs->x, y / rhs->y); }
    Vector2^ operator*(float rhs) { return gcnew Vector2(x * rhs, y * rhs); }

    float Length() { return sqrtf(x * x + y * y); }
    float LengthSquared() { return x * x + y * y; }
    float DotProduct(Vector2^ rhs) { return x * rhs->x + y * rhs->y; }
    
    void Normalize()
    {
        float lenSquared = LengthSquared();
        if (!Urho3D::Equals(lenSquared, 1.0f) && lenSquared > 0.0f)
        {
            float invLen = 1.0f / sqrtf(lenSquared);
            x *= invLen;
            y *= invLen;
        }
    }

    Vector2^ Normalized()
    {
        float lenSquared = LengthSquared();
        if (!Urho3D::Equals(lenSquared, 1.0f) && lenSquared > 0.0f)
        {
            float invLen = 1.0f / sqrtf(lenSquared);
            return *this * invLen;
        }
        else
            return gcnew Vector2(this);
    }

    System::String^ ToString() override
    {
        System::String^ ret = gcnew System::String("");
        ret += x;
        ret += " ";
        ret += y;
        return ret;
    }
};

public ref class Vector3 : public MathVector
{
public:
    property float x;
    property float y;
    property float z;

    Vector3();
    Vector3(Vector3^ src) : MathVector(3) { x = src->x; y = src->y; z = src->z; }
    Vector3(float xx, float yy, float zz) : MathVector(3) { x = xx; y = yy; z = zz; }
    Vector3(Urho3D::Vector3 vec);
    Urho3D::Vector3 ToVector3();

    virtual MathVector^ Clone() override {
        return gcnew Vector3(this);
    }

    virtual float Get(int index) override {
        if (index == 0)
            return x;
        if (index == 1)
            return y;
        return z;
    }
    virtual void Set(int index, float value) override {
        if (index == 0)
            x = value;
        if (index == 1)
            y = value;
        z = value;
    }

    Vector3^ operator+=(Vector3^ rhs) { x += rhs->x; y += rhs->y; z += rhs->z; return this; }
    Vector3^ operator-=(Vector3^ rhs) { x -= rhs->x; y -= rhs->y; z -= rhs->z; return this; }
    Vector3^ operator*=(Vector3^ rhs) { x *= rhs->x; y *= rhs->y; z *= rhs->z; return this; }
    Vector3^ operator*=(float rhs) { x *= rhs; y *= rhs;  z *= rhs; return this; }
    Vector3^ operator/=(Vector3^ rhs) { x /= rhs->x; y /= rhs->y; z /= rhs->z; return this; }

    Vector3^ operator+(float rhs) { return gcnew Vector3(x + rhs, y + rhs, z + rhs); }
    Vector3^ operator+(Vector3^ rhs) { return gcnew Vector3(x + rhs->x, y + rhs->y, z + rhs->z); }
    Vector3^ operator-(Vector3^ rhs) { return gcnew Vector3(x - rhs->x, y - rhs->y, z - rhs->z); }
    Vector3^ operator*(Vector3^ rhs) { return gcnew Vector3(x * rhs->x, y * rhs->y, z * rhs->z); }
    Vector3^ operator/(Vector3^ rhs) { return gcnew Vector3(x / rhs->x, y / rhs->y, z * rhs->z); }
    Vector3^ operator*(float rhs) { return gcnew Vector3(x * rhs, y * rhs, z * rhs); }

    void Normalize()
    {
        float lenSquared = LengthSquared();
        if (!Urho3D::Equals(lenSquared, 1.0f) && lenSquared > 0.0f)
        {
            float invLen = 1.0f / sqrtf(lenSquared);
            x *= invLen;
            y *= invLen;
            z *= invLen;
        }
    }

    float Length() { return sqrtf(x * x + y * y + z * z); }
    float LengthSquared() { return x * x + y * y + z * z; }
    /// Calculate dot product.
    float DotProduct(Vector3^ rhs) { return x * rhs->x + y * rhs->y + z * rhs->z; }

    /// Calculate cross product.
    Vector3^ CrossProduct(Vector3^ rhs)
    {
        return gcnew Vector3(
            y * rhs->z - z * rhs->y,
            z * rhs->x - x * rhs->z,
            x * rhs->y - y * rhs->x
            );
    }

    Vector3^ Normalized()
    {
        float lenSquared = LengthSquared();
        if (!Urho3D::Equals(lenSquared, 1.0f) && lenSquared > 0.0f)
        {
            float invLen = 1.0f / sqrtf(lenSquared);
            return this * invLen;
        }
        else
            return gcnew Vector3(this->x, this->y, this->z);
    }

    float Angle(Vector3^ rhs) { return Urho3D::Acos(DotProduct(rhs) / (Length() * rhs->Length())); }

    Vector3^ Lerp(Vector3^ rhs, float t) { return *this * (1.0f - t) + rhs * t; }

    System::String^ ToString() override
    {
        System::String^ ret = gcnew System::String("");
        ret += x;
        ret += " ";
        ret += y;
        ret += " ";
        ret += z;
        return ret;
    }
};


public ref class Vector4 : public MathVector
{
public:
    property float x;
    property float y;
    property float z;
    property float w;

    Vector4() : MathVector(4) { x = 0; y = 0; z = 0; w = 1.0f; }
    Vector4(Vector4^ src) : MathVector(4) { x = src->x; y = src->y; z = src->z; w = src->w; }
    Vector4(float xx, float yy, float zz, float ww) : MathVector(4) { x = xx; y = yy; z = zz; w = ww; }
    Vector4(Urho3D::Vector4 vec) : MathVector(4) { x = vec.x_; y = vec.y_; z = vec.z_; w = vec.w_; }
    Urho3D::Vector4 ToVector4() { return Urho3D::Vector4(x, y, z, w); }

    virtual MathVector^ Clone() override {
        return gcnew Vector4(this);
    }

    virtual float Get(int index) override {
        if (index == 0)
            return x;
        if (index == 1)
            return y;
        if (index == 2)
            return z;
        return w;
    }

    virtual void Set(int index, float value) override {
        if (index == 0)
            x = value;
        if (index == 1)
            y = value;
        if (index == 2)
            z = value;
        w = value;
    }
};


public ref class Quaternion
{
    Urho3D::Quaternion* quat;
public:
    property float x { float get() { return quat->x_; } void set(float value) { quat->x_ = value; } }
    property float y { float get() { return quat->y_; } void set(float value) { quat->y_ = value; } }
    property float z { float get() { return quat->z_; } void set(float value) { quat->z_ = value; } }
    property float w { float get() { return quat->w_; } void set(float value) { quat->w_ = value; } }

    Quaternion() { quat = new Urho3D::Quaternion(); }
    ~Quaternion() { delete quat; }
    Quaternion(float ww, float xx, float yy, float zz) { quat = new Urho3D::Quaternion(ww, xx, yy, zz); }

    Quaternion(Quaternion^ rhs) { quat = new Urho3D::Quaternion(*(rhs->quat)); }

    Quaternion(float pitch, float yaw, float roll)
    {
        quat = new Urho3D::Quaternion(pitch, yaw, roll);
    }

    Quaternion(float angle, Vector3^ axis)
    {
        quat = new Urho3D::Quaternion(angle, axis->ToVector3());
    }

    Quaternion(Urho3D::Quaternion q) { quat = new Urho3D::Quaternion(q); }

    Urho3D::Quaternion ToQuaternion() { return *quat; }

    void FromEulerAngles(float x, float y, float z)
    {
        quat->FromEulerAngles(x, y, z);
    }

    void FromAngleAxis(float angle, Vector3^ axis)
    {
        quat->FromAngleAxis(angle, axis->ToVector3());
    }

    Vector3^ ToEulerAngles() {
        return gcnew Vector3(quat->EulerAngles());
    }

    /// Assign from another quaternion.
    Quaternion^ operator = (Quaternion^ rhs)
    {
        *quat = *rhs->quat;
        return this;
    }

    /// Add-assign a quaternion.
    Quaternion^ operator += (Quaternion^ rhs)
    {
        *quat += *rhs->quat;
        return this;
    }

    Quaternion^ operator * (float rhs) { return gcnew Quaternion(w * rhs, x * rhs, y * rhs, z * rhs); }
    Quaternion^ operator - () { return gcnew Quaternion(-w, -x, -y, -z); }
    Quaternion^ operator + (Quaternion^ rhs) { return gcnew Quaternion(w + rhs->w, x + rhs->x, y + rhs->y, z + rhs->z); }
    Quaternion^ operator - (Quaternion^ rhs) { return gcnew Quaternion(w - rhs->w, x - rhs->x, y - rhs->y, z - rhs->z); }

    Quaternion^ operator * (Quaternion^ rhs)
    {
        return gcnew Quaternion(*quat * *rhs->quat);
    }

    Vector3^ operator * (Vector3^ rhs)
    {
        //return *quat * rhs->ToVector3();
        Vector3^ qVec = gcnew Vector3(x, y, z);
        Vector3^ cross1(qVec->CrossProduct(rhs));
        Vector3^ cross2(qVec->CrossProduct(cross1));
        return rhs + (cross1 * w + cross2) * 2.0f;
    }

};


public ref class Color : MathVector
{
public:
    property float r;
    property float g;
    property float b;
    property float a;

    Color() : MathVector(4) { r = 0; g = 0; b = 0; a = 0.0f; }
    Color(float rr, float gg, float bb, float aa) : MathVector(4) { r = rr; g = gg; b = bb; a = aa; }
    Color(Urho3D::Color col) : MathVector(4) { r = col.r_; g = col.g_; b = col.b_; a = col.a_; }
    Urho3D::Color ToColor() { return Urho3D::Color(r, g, b, a); }

    virtual float Get(int index) override
    {
        if (index == 0)
            return r;
        if (index == 1)
            return g;
        if (index == 2)
            return b;
        return a;
    }

    virtual void Set(int index, float val) override
    {
        if (index == 0)
            r = val;
        if (index == 1)
            g = val;
        if (index == 2)
            b = val;
        if (index == 4)
            a = val;
    }

    Color^ operator+(Color^ rhs) {
        return gcnew Color(r + rhs->r, g + rhs->g, b + rhs->b, a + rhs->a);
    }
    Color^ operator-(Color^ rhs)
    {
        return gcnew Color(r - rhs->r, g - rhs->g, b - rhs->b, a - rhs->a);
    }
    Color^ operator*(float rhs)
    {
        return gcnew Color(r * rhs, g *rhs, b*rhs, a*rhs);
    }
    Color^ operator+=(Color^ rhs)
    {
        r += rhs->r;
        g += rhs->g;
        b += rhs->b;
        a += rhs->a;
        return this;
    }
    Color^ operator-=(Color^ rhs)
    {
        r -= rhs->r;
        g -= rhs->g;
        b -= rhs->b;
        a -= rhs->a;
        return this;
    }
};


public ref class IntVector2 : MathVector
{
public:
    property int x;
    property int y;

    IntVector2() : MathVector(2) { x = 0; y = 0; }
    IntVector2(int xx, int yy) : MathVector(2) { x = xx; y = yy; }
    IntVector2(Urho3D::IntVector2 vec) : MathVector(2) { x = vec.x_; y = vec.y_; }
    Urho3D::IntVector2 ToIntVector2() { return Urho3D::IntVector2(x, y); }

    virtual float Get(int index) override {
        if (index == 0)
            return (float)x;
        return (float)y;
    }
    virtual void Set(int index, float value) override {
        if (index == 0)
            x = (int)value;
        y = (int)value;
    }

    IntVector2^ operator+=(IntVector2^ rhs) { x += rhs->x; y += rhs->y; return this; }
    IntVector2^ operator-=(IntVector2^ rhs) { x -= rhs->x; y -= rhs->y; return this; }
    IntVector2^ operator*=(IntVector2^ rhs) { x *= rhs->x; y *= rhs->y; return this; }
    IntVector2^ operator/=(IntVector2^ rhs) { x /= rhs->x; y /= rhs->y; return this; }

    IntVector2^ operator+(IntVector2^ rhs) {
        return gcnew IntVector2(x + rhs->x, y + rhs->y);
    }
    IntVector2^ operator-(IntVector2^ rhs) {
        return gcnew IntVector2(x - rhs->x, y - rhs->y);
    }
    IntVector2^ operator*(IntVector2^ rhs) {
        return gcnew IntVector2(x * rhs->x, y * rhs->y);
    }
    IntVector2^ operator/(IntVector2^ rhs) {
        return gcnew IntVector2(x / rhs->x, y / rhs->y);
    }

    System::String^ ToString() override
    {
        System::String^ ret = gcnew System::String("");
        ret += x;
        ret += " ";
        ret += y;
        return ret;
    }
};

public ref class Rect : MathVector
{
public:
    property float left;
    property float top;
    property float right;
    property float bottom;

    Rect(float ll, float tt, float rr, float bb) : MathVector(4) { left = ll; right = rr; top = tt; bottom = bb; }
    Rect() : MathVector(4) { left = 0; right = 0; top = 0; bottom = 0; }
    Rect(Urho3D::Rect r) : MathVector(4) { left = r.min_.x_; right = r.max_.x_; top = r.max_.y_; bottom = r.min_.y_; }
    Urho3D::Rect ToRect() { return Urho3D::Rect(left, top, right, bottom); }

    virtual float Get(int index) override 
    { 
        if (index == 0)
            return left;
        else if (index == 1)
            return top;
        else if (index == 2)
            return right;
        return bottom; 
    }
    virtual void Set(int index, float value) override
    {
        if (index == 0)
            left = value;
        else if (index == 1)
            top = value;
        else if (index == 2)
            right = value;
        else
            bottom = value;
    }
};


public ref class IntRect : MathVector
{
public:
    property int left;
    property int top;
    property int right;
    property int bottom;

    IntRect(int ll, int tt, int rr, int bb) : MathVector(4) { left = ll; right = rr; top = tt; bottom = bb; }
    IntRect() : MathVector(4) { left = 0; right = 0; top = 0; bottom = 0; }
    IntRect(Urho3D::IntRect r) : MathVector(4) { left = r.left_; right = r.right_; top = r.top_; bottom = r.bottom_; }
    Urho3D::IntRect ToIntRect() { return Urho3D::IntRect(left, top, right, bottom); }

    virtual float Get(int index) override
    {
        if (index == 0)
            return (float)left;
        else if (index == 1)
            return (float)top;
        else if (index == 2)
            return (float)right;
        return (float)bottom;
    }
    virtual void Set(int index, float value) override
    {
        if (index == 0)
            left = (int)value;
        else if (index == 1)
            top = (int)value;
        else if (index == 2)
            right = (int)value;
        else
            bottom = (int)value;
    }
};

public ref class MathMatrix
{
public:
    MathMatrix(int rows, int columns) { 
        Rows = rows; Columns = columns; 
        data_ = new float[rows*columns]; 
        for (int i = 0; i < rows*columns; ++i)
            data_[i] = 0.0f;
    }
    ~MathMatrix() { delete[] data_; }

    virtual float Get(int index) { return data_[index]; }
    virtual void Set(int index, float value) { data_[index] = value; }

    property int Rows;
    property int Columns;

    float* data_;
};

public ref class Matrix3 : public MathMatrix
{
public:
    Matrix3() : MathMatrix(3, 3) { }
    Matrix3(Urho3D::Matrix3 mat) : Matrix3()
    {
        for (int i = 0; i < Rows*Columns; ++i)
            data_[i] = mat.Data()[i];
    }

    Urho3D::Matrix3 ToMatrix3() {
        Urho3D::Matrix3 ret;
        memcpy((void*)ret.Data(), data_, Rows * Columns * sizeof(float));
        return ret;
    }
};

public ref class Matrix3x4 : public MathMatrix
{
public:
    Matrix3x4() : MathMatrix(3, 4) {}
    Matrix3x4(Urho3D::Matrix3x4 mat) : Matrix3x4()
    {
        for (int i = 0; i < Rows*Columns; ++i)
            data_[i] = mat.Data()[i];
    }

    Urho3D::Matrix3x4 ToMatrix3x4() {
        Urho3D::Matrix3x4 ret;
        memcpy((void*)ret.Data(), data_, Rows * Columns * sizeof(float));
        return ret;
    }
};

public ref class Matrix4 : public MathMatrix
{
public:
    Matrix4() : MathMatrix(4, 4) {}
    Matrix4(Urho3D::Matrix4 mat) : Matrix4()
    {
        for (int i = 0; i < Rows*Columns; ++i)
            data_[i] = mat.Data()[i];
    }

    Urho3D::Matrix4 ToMatrix4() {
        Urho3D::Matrix4 ret;
        memcpy((void*)ret.Data(), data_, Rows * Columns * sizeof(float));
        return ret;
    }
};

}