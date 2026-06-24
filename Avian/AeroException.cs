// // -----------------------------------------------------------------------
// // <copyright file="AeroException.cs" company="ThetaRP">
// // Copyright (c) Spicco D'Aura. All rights reserved.
// // Licensed under the CC BY-SA 3.0 license.
// // </copyright>
// // -----------------------------------------------------------------------

namespace Avian;

public class AeroException(string message) : Exception(message);