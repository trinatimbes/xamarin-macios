#!/bin/bash -e

set -o pipefail

if test -n "$V"; then set +x; fi
if test -z "$TOP"; then echo "TOP not set"; exit 1; fi
if test -z "$DOTNET_DESTDIR"; then echo "DOTNET_DESTDIR not set"; exit 1; fi
if test -z "$MAC_DESTDIR"; then echo "MAC_DESTDIR not set"; exit 1; fi
if test -z "$IOS_DESTDIR"; then echo "IOS_DESTDIR not set"; exit 1; fi
if test -z "$MAC_FRAMEWORK_DIR"; then echo "MAC_FRAMEWORK_DIR not set"; exit 1; fi
if test -z "$MONOTOUCH_PREFIX"; then echo "MONOTOUCH_PREFIX not set"; exit 1; fi

cp="cp -c"

# the Xamarin.*OS.Lite.Sdk nugets
create_sdk_nugets ()
{
	local platform=$1
	local legacy_destdir=$2
	#shellcheck disable=SC2155
	local platform_lower=$(echo "$platform" | tr '[:upper:]' '[:lower:]')
	local packageid=Xamarin.$platform.Sdk.Lite
	local destdir=$DOTNET_DESTDIR/$packageid

	rm -Rf "$destdir"
	mkdir -p "$destdir/tools"
	mkdir -p "$destdir/targets"
	mkdir -p "$destdir/Sdk"

	$cp "$legacy_destdir/Version" "$destdir/"
	$cp "$legacy_destdir/buildinfo" "$destdir/tools/"

	$cp "$TOP/msbuild/dotnet/Xamarin.$platform.Sdk.Lite/Sdk/"* "$destdir/Sdk/"
	$cp "$TOP/msbuild/dotnet/targets/"* "$destdir/targets/"
	$cp "$TOP/msbuild/dotnet/Xamarin.$platform.Sdk.Lite/targets/"* "$destdir/targets/"

	$cp -r "$legacy_destdir/lib/msbuild" "$destdir/tools/"

	chmod -R +r "$destdir"
}

create_sdk_nugets  "iOS"     "$IOS_DESTDIR$MONOTOUCH_PREFIX"
# create_sdk_nugets  "tvOS"    "$MAC_DESTDIR$MAC_FRAMEWORK_DIR/Versions/Current"
# create_sdk_nugets  "watchOS" "$IOS_DESTDIR$MONOTOUCH_PREFIX"
# create_sdk_nugets  "macOS"   "$IOS_DESTDIR$MONOTOUCH_PREFIX"
