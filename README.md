# Fresnel ModelTypes

Fresnel ModelTypes is a .NET library containing Interfaces and Classes for building DDD applications.

This library is designed for use with the [**Fresnel Domain Model Explorer**](https://github.com/Envivo-Software/Envivo.Fresnel) prototyping and modelling tool,
but may be used independently to create your own DDD class models.

You can find usage examples in the following projects:
- [Fresnel.Sample.ShoppingProject](https://github.com/Envivo-Software/Fresnel.Sample.ShoppingProject)
- [Fresnel.Sample.Features](https://github.com/Envivo-Software/Fresnel.Sample.Features)

## Namespaces

### Envivo.Fresnel.ModelTypes

These are mostly abstract base classes, and can be a good starting point if you don't want the hassle of implementing interfaces from scratch.

Objects and Collections are designed to raise notifications, which could be handy if you are binding objects directly to views.

### Envivo.Fresnel.ModelTypes.Interfaces

If you're restricting from using base classes in your code model (or prefer hand-crafting your own)	, consider using the interfaces within this namespace.

*Copyright 2022-2025 Envivo Software*