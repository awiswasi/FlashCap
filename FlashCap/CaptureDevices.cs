﻿////////////////////////////////////////////////////////////////////////////
//
// FlashCap - Independent camera capture library.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using FlashCap.Devices;
using FlashCap.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCap
{
    public class CaptureDevices
    {
        public virtual IEnumerable<CaptureDeviceDescriptor> EnumerateDescriptors() =>
            NativeMethods.CurrentPlatform switch
            {
                NativeMethods.Platforms.Windows =>
                    new DirectShowDevices().EnumerateDescriptors().
                    Concat(new VideoForWindowsDevices().EnumerateDescriptors()),
                NativeMethods.Platforms.Linux =>
                    new V4L2Devices().EnumerateDescriptors(),
                _ =>
                    ArrayEx.Empty<CaptureDeviceDescriptor>(),
            };
    }
}
