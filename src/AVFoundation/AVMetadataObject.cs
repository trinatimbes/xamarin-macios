// 
// AVMetadataObject.cs:
//     
// Copyright 2014 Xamarin Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#if IOS
using Foundation;
using ObjCRuntime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AVFoundation {

	public partial class AVMetadataObject {
		public AVMetadataObjectType Type {
			get {
				return AVMetadataObjectTypeExtensions.GetValue (WeakType);
			}
		}

		internal static AVMetadataObjectType ArrayToEnum (NSString[] arr)
		{
			AVMetadataObjectType rv = AVMetadataObjectType.None;

			if (arr == null || arr.Length == 0)
				return rv;

			foreach (var str in arr) {
				rv |= AVMetadataObjectTypeExtensions.GetValue (str);
			}

			return rv;
		}

		internal static NSString[] EnumToArray (AVMetadataObjectType value)
		{
			if (value == AVMetadataObjectType.None)
				return null;

			var rv = new List<NSString> ();
			var val = (ulong) value;
			var shifts = 0;

			while (val != 0) {
				if ((val & 0x1) == 0x1)
					rv.Add (((AVMetadataObjectType) (0x1UL << shifts)).GetConstant ());
				val >>= 1;
				shifts++;
			}

			return rv.ToArray ();
		}
	}
}
#endif
